using EliteHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using Wms.Web.Api.Service.Entity;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// PDA相关服务接口
    /// </summary>
    [RoutePrefix("api/PdaService")]
    [EnableCors(origins: "http://192.168.60.29:10870", headers: "*", methods: "*")]
    public class PdaServiceController : ApiController
    {
        private static Semaphore MutexSemaphore { get; set; }
        static PdaServiceController()
        {
            //初始信号量值为1，最大信号量值为1  
            MutexSemaphore = new Semaphore(1, 1);
        }

        #region ---用户登陆校验
        /// <summary>
        /// 登陆用户校验申请
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginUserValidation")]
        public UserValidationResponseBody LoginUserValidation([FromBody] UserValidaitonRequestBody reqBody)
        {
            UserValidationResponseBody respnse = new UserValidationResponseBody();
            try
            {
                string pwd = CommonFunc.GetMD5_32(reqBody.LoginPwd);
                DataTable table = SqlDbHelper.GetDataSet($"select a.*,b.F_RoleName from T_Base_User a left outer join T_Base_Role b on a.F_RelationRole=b.F_ID where a.F_UserCode='{reqBody.UserCode}'").Tables[0];

                if (table == null)
                {
                    throw new Exception("获取登陆用户信息失败，请检查网络连接或与开发人员联系.");
                }

                if (table.Rows.Count == 0)
                {
                    throw new Exception($"用户 {reqBody.UserCode} 不存在.");
                }

                if (!pwd.Equals(table.Rows[0]["F_LoginPwd"].ToString().Trim()))
                {
                    throw new Exception("登陆密码不正确.");
                }

                if (!table.Rows[0]["F_IsEffective"].ToString().ToLower().Equals("use"))
                {
                    throw new Exception($"用户 {reqBody.UserCode} 已被禁用.");
                }

                respnse.Data = new UserBody
                {
                    ID = Convert.ToInt64(table.Rows[0]["F_ID"]),
                    UserCode = table.Rows[0]["F_UserCode"].ToString(),
                    UserName = table.Rows[0]["F_UserName"].ToString(),
                    LoginPwd = reqBody.LoginPwd,
                    RelationRoleID = Convert.ToInt64(table.Rows[0]["F_RelationRole"]),
                    RelationRoleName = table.Rows[0]["F_RoleName"].ToString()
                };
            }
            catch (Exception ex)
            {
                respnse.Code = "500";
                respnse.Msg = ex.Message;
                respnse.Data = null;
            }
            return respnse;
        }
        #endregion

        #region ---获取仓库信息
        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWarehouse")]
        public WarehouseQueryResponseBody GetWarehouse()
        {
            WarehouseQueryResponseBody body = new WarehouseQueryResponseBody();
            try
            {
                DataTable table = SqlDbHelper.GetDataSet("select * from T_Base_Warehouse where F_ID<>5 and F_IsEffective='use'").Tables[0] ?? 
                    throw new Exception("获取仓库信息失败，请检查网络连接或与开发人员联系.");

                List<WarehouseBody> list = new List<WarehouseBody>();

                foreach (DataRow row in table.Rows)
                {
                    WarehouseBody wh = new WarehouseBody
                    {
                        WarehouseID = Convert.ToInt64(row["F_ID"]),
                        WarehouseCode = row["F_Code"].ToString(),
                        WarehouseName = row["F_Name"].ToString()
                    };
                    list.Add(wh);
                }

                body.Data = list;
            }
            catch (Exception ex)
            {
                body.Code = "500";
                body.Msg = ex.Message;
                body.Data = null;
            }
            return body;
        }
        #endregion

        #region ---获取站台信息
        /// <summary>
        /// 获取仓库站台信息
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStationByWarehouse")]
        public StationQueryResponseBody GetStationByWarehouse([FromBody] StationQueryRequestBody body)
        {
            StationQueryResponseBody response = new StationQueryResponseBody();
            try
            { 
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                    response.Data = null;
                }
                else
                {
                    int stationType = body.StationType.ToLower().Equals("in") ? 3 : 4;
                    DataTable table = SqlDbHelper.GetDataSet($"select F_ID,F_CellCode from T_Base_StoreCell where F_WarehouseID={body.WarehouseID} and F_StationType={stationType} and F_CellType='Station' order by F_ID desc").Tables[0] ?? 
                        throw new Exception("获取站台信息失败，请检查网络连接或与开发人员联系.");

                    List<StationBody> stations = new List<StationBody>();
                    foreach (DataRow row in table.Rows)
                    {
                        StationBody station = new StationBody
                        {
                            StationID = Convert.ToInt64(row["F_ID"]),
                            StationName = row["F_CellCode"].ToString()
                        };

                        stations.Add(station);
                    }

                    response.Data = stations;
                }
            }
            catch(Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }
        #endregion

        #region ---获取端子库、端子模具库计划信息
        /// <summary>
        /// 获取全部处于等待或执行状态的端子、模具生产领料主计划信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTerminalIssuePlan")]
        public PlanQueryResponseBody GetTerminalIssuePlan()
        {
            PlanQueryResponseBody response = new PlanQueryResponseBody();
            try
            {
                DataTable tablePlan = SqlDbHelper.GetDataSet($"select * from v_Project_Plan where F_PlanType='101' and F_AutoFlag=0 and F_PlanStatus in ('Wait','Execute')").Tables[0] ??
                    throw new Exception("获取计划信息失败，请检查网络连接或与开发人员联系.");

                List<PlanBody> list = new List<PlanBody>();

                foreach(DataRow row in tablePlan.Rows)
                {
                    PlanBody plan = new PlanBody
                    {
                        PlanID = Convert.ToInt64(row["F_PlanID"]),
                        AutoFlag = Convert.ToInt32(row["F_AutoFlag"]),
                        LocalPlanCode = row["F_LocalPlanCode"].ToString(),
                        RemotePlanCode = row["F_RemotePlanCode"].ToString(),
                        PlanType = row["F_PlanType"].ToString(),
                        PlanTypeName = row["F_PlanTypeName"].ToString(),
                        PlanStatus = row["F_PlanStatus"].ToString(),
                        PlanStatusName = row["F_PlanStatusName"].ToString(),
                        StationID = Convert.ToInt64(row["F_StationID"]),
                        StationName = row["F_StationName"].ToString(),
                        CreateTime = row["F_CreateTime"].ToString(),
                        FromWarehouse = row["F_FromWarehouse"].ToString()
                    };

                    list.Add(plan);
                }

                response.Data = list;
            }
            catch(Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }

        /// <summary>
        /// 获取给定计划编号的明细信息
        /// </summary>
        /// <param name="planCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPlanList")]
        public PlanListQueryResponseBody GetPlanList(string planCode)
        {
            PlanListQueryResponseBody response = new PlanListQueryResponseBody();
            try
            {
                if (string.IsNullOrWhiteSpace(planCode))
                {
                    throw new Exception("计划编号不能为空.");
                }

                DataTable tablePlanList = SqlDbHelper.GetDataSet($"select * from v_Project_PlanList where F_LocalPlanCode='{planCode}'").Tables[0] ??
                    throw new Exception("获取计划明细信息失败，请检查网络连接或与开发人员联系.");

                List<PlanListBody> list = new List<PlanListBody>();

                foreach (DataRow row in tablePlanList.Rows)
                {
                    PlanListBody body = new PlanListBody
                    {
                        PlanListID = Convert.ToInt64(row["F_PlanListID"]),
                        MaterialID = Convert.ToInt64(row["F_MaterialID"]),
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        MaterialName = row["F_MaterialName"].ToString(),
                        MaterialBarcode = row["F_MaterialBarcode"].ToString(),
                        MaterialCategoryID = Convert.ToInt64(row["F_MaterialCategoryID"]),
                        MaterialCategoryName = row["F_MaterialCategoryName"].ToString(),
                        Qty = Convert.ToDecimal(row["F_Qty"]),
                        ConsumedQty = Convert.ToDecimal(row["F_ConsumedQty"]),
                        EmptyFlag = row["F_EmptyFlag"].ToString(),
                        PackingCategory = Convert.ToInt64(row["F_PackingCategory"]),
                        PackingCategoryName = row["F_PackingCategoryName"].ToString(),
                        PlanListStatus = row["F_PlanListStatus"].ToString(),
                        PlanListStatusName = row["F_PlanListStatusName"].ToString(),
                        BeginTime = row["F_BeginTime"].ToString(),
                        CompleteTime = row["F_CompleteTime"].ToString()
                    };
                    list.Add(body);
                }
                response.Data = list;                
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }

        /// <summary>
        /// 获取给定计划编号分配的物料清单信息
        /// </summary>
        /// <param name="planCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PlanSeqQuery")]
        public PlanSeqQueryResponseBody PlanSeqQuery(string planCode)
        {
            PlanSeqQueryResponseBody response = new PlanSeqQueryResponseBody();

            try
            {
                if (string.IsNullOrWhiteSpace(planCode))
                {
                    throw new Exception("计划编号不能为空.");
                }

                DataTable tablePlanSeq = SqlDbHelper.GetDataSet($"select * from v_Project_PlanListSeq where F_LocalPlanCode='{planCode}' and F_PlanType='101' and F_AutoFlag=0").Tables[0] ??
                    throw new Exception("获取计划分配的物料清单信息失败，请检查网络连接或与开发人员联系.");

                List<PlanSeqQueryMaterialBody> list = new List<PlanSeqQueryMaterialBody>();

                foreach (DataRow row in tablePlanSeq.Rows)
                {
                    PlanSeqQueryMaterialBody material = new PlanSeqQueryMaterialBody
                    {
                        WarehouseID = Convert.ToInt64(row["F_WarehouseID"]),
                        WarehouseName = row["F_WarehouseName"].ToString(),
                        StoreCellCode = row["F_StoreCellCode"].ToString(),
                        MaterialID = Convert.ToInt64(row["F_MaterialID"]),
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        MaterialName = row["F_MaterialName"].ToString(),
                        MaterialBarcode = row["F_MaterialBarcode"].ToString(),
                        BatchNo = row["F_BatchNo"].ToString(),
                        Qty = Convert.ToDecimal(row["F_Qty"])
                    };

                    list.Add(material);
                }

                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }

            return response;
        }
        /// <summary>
        /// 获取给定计划编号的手工领料单分配的物料清单信息
        /// </summary>
        /// <param name="planCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManuallyPlanSeqQuery")]
        public PlanSeqQueryResponseBody ManuallyPlanSeqQuery(string planCode)
        {
            PlanSeqQueryResponseBody response = new PlanSeqQueryResponseBody();

            try
            {
                if (string.IsNullOrWhiteSpace(planCode))
                {
                    throw new Exception("计划编号不能为空.");
                }

                DataTable tablePlanSeq = SqlDbHelper.GetDataSet($"select * from v_Project_PlanListSeq where F_LocalPlanCode='{planCode}' and F_PlanType='103'").Tables[0] ??
                    throw new Exception("获取计划分配的物料清单信息失败，请检查网络连接或与开发人员联系.");

                List<PlanSeqQueryMaterialBody> list = new List<PlanSeqQueryMaterialBody>();

                foreach (DataRow row in tablePlanSeq.Rows)
                {
                    PlanSeqQueryMaterialBody material = new PlanSeqQueryMaterialBody
                    {
                        WarehouseID = Convert.ToInt64(row["F_WarehouseID"]),
                        WarehouseName = row["F_WarehouseName"].ToString(),
                        StoreCellCode = row["F_StoreCellCode"].ToString(),
                        MaterialID = Convert.ToInt64(row["F_MaterialID"]),
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        MaterialName = row["F_MaterialName"].ToString(),
                        MaterialBarcode = row["F_MaterialBarcode"].ToString(),
                        BatchNo = row["F_BatchNo"].ToString(),
                        Qty = Convert.ToDecimal(row["F_Qty"])
                    };

                    list.Add(material);
                }

                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }

            return response;
        }
        #endregion

        #region ---获取手工领料计划信息
        /// <summary>
        /// 获取全部处于等待或执行状态的手工领料主计划信息
        /// </summary>
        /// <param name="warehouse">仓库信息；1：导线库  2：端子库</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetManuallyIssuePlan")]
        public PlanQueryResponseBody GetManuallyIssuePlan(int warehouse)
        {
            PlanQueryResponseBody response = new PlanQueryResponseBody();
            try
            {
                int autoFlag = warehouse.Equals(1) ? 1 : 0;
                DataTable tablePlan = SqlDbHelper.GetDataSet($"select * from v_Project_Plan where F_PlanType='103' and F_AutoFlag={autoFlag} and F_PlanStatus in ('Wait','Execute')").Tables[0] ??
                    throw new Exception("获取计划信息失败，请检查网络连接或与开发人员联系.");

                List<PlanBody> list = new List<PlanBody>();

                foreach (DataRow row in tablePlan.Rows)
                {
                    PlanBody plan = new PlanBody
                    {
                        PlanID = Convert.ToInt64(row["F_PlanID"]),
                        AutoFlag = Convert.ToInt32(row["F_AutoFlag"]),
                        LocalPlanCode = row["F_LocalPlanCode"].ToString(),
                        RemotePlanCode = row["F_RemotePlanCode"].ToString(),
                        PlanType = row["F_PlanType"].ToString(),
                        PlanTypeName = row["F_PlanTypeName"].ToString(),
                        PlanStatus = row["F_PlanStatus"].ToString(),
                        PlanStatusName = row["F_PlanStatusName"].ToString(),
                        StationID = Convert.ToInt64(row["F_StationID"]),
                        StationName = row["F_StationName"].ToString(),
                        CreateTime = row["F_CreateTime"].ToString(),
                        FromWarehouse = row["F_FromWarehouse"].ToString()
                    };

                    list.Add(plan);
                }

                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }
        #endregion

        #region ---物料入库
        /// <summary>
        /// 原材料初始入库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialInbound")]
        public WmsResponseBody MaterialInbound([FromBody] MaterialInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证给定的仓库索引是否正确
                    if (!body.WarehouseID.Equals(1) && !body.WarehouseID.Equals(2))
                        throw new Exception("仓库索引不正确.");

                    //验证给定的条码是否为空
                    if (string.IsNullOrWhiteSpace(body.MaterialBarcode))
                        throw new Exception("物料条码不能为空.");

                    string materialBarcode = CommonFunc.BarcodeProcess(body.MaterialBarcode);

                    //调用厂级wms接口，获取标签信息
                    List<string> barcodes = new List<string>
                    {
                        materialBarcode
                    };

                    DataTable table = CommonFunc.GetBarcodeDetail(barcodes);
                    DataRow[] rows = table.Select($"LabelNo='{materialBarcode}'");

                    string materialCode = rows[0]["ItemCode"].ToString().Trim();
                    string batchNo = rows[0]["BatchNo"].ToString().Trim();
                    decimal qty = Convert.ToDecimal(rows[0]["Qty"]);

                    //数量校验
                    if (qty <= 0)
                    {
                        throw new Exception($"物料数量不正确；查询到的数量为{qty}");
                    }

                    //提交初始入库
                    CommonFunc.MaterialInbound(body.WarehouseID, body.StationID, materialBarcode, materialCode, batchNo, qty, body.OperatorID);
                }
            }
            catch(Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 原材料盘点入库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialCheckInbound")]
        public WmsResponseBody MaterialCheckInbound([FromBody] MaterialInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证给定的仓库索引是否正确
                    if (!body.WarehouseID.Equals(1) && !body.WarehouseID.Equals(2))
                        throw new Exception("仓库索引不正确.");

                    //验证给定的条码是否为空
                    if (string.IsNullOrWhiteSpace(body.MaterialBarcode))
                        throw new Exception("物料条码不能为空.");

                    string materialBarcode = CommonFunc.BarcodeProcess(body.MaterialBarcode);

                    //提交盘点入库
                    CommonFunc.MaterialCheckInbound(body.WarehouseID, body.StationID, materialBarcode, body.OperatorID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 原材料退料入库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialReturnInbound")]
        public WmsResponseBody MaterialReturnInbound([FromBody] MaterialInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证给定的仓库索引是否正确
                    if (!body.WarehouseID.Equals(1) && !body.WarehouseID.Equals(2))
                        throw new Exception("仓库索引不正确.");

                    //验证给定的条码是否为空
                    if (string.IsNullOrWhiteSpace(body.MaterialBarcode))
                        throw new Exception("物料条码不能为空.");

                    string materialBarcode = CommonFunc.BarcodeProcess(body.MaterialBarcode);

                    //获取生产消耗数
                    decimal qty = CommonFunc.GetMaterialConsumedQty(materialBarcode);

                    //提交退料入库
                    CommonFunc.MaterialReturnInbound(body.WarehouseID, body.StationID, materialBarcode, qty, body.OperatorID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 空轴处理
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProcessEmptyMaterial")]
        public WmsResponseBody ProcessEmptyMaterial([FromBody] MaterialInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证给定的条码是否为空
                    if (string.IsNullOrWhiteSpace(body.MaterialBarcode))
                        throw new Exception("物料条码不能为空.");

                    string materialBarcode = CommonFunc.BarcodeProcess(body.MaterialBarcode);

                    //获取生产消耗数
                    decimal qty = CommonFunc.GetMaterialConsumedQty(materialBarcode);

                    //提交空轴处理
                    CommonFunc.ProcessEmptyMaterial(materialBarcode, qty, body.OperatorID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion

        #region ---手工执行计划与组箱操作
        /// <summary>
        /// 执行手工领料计划
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExecuteManuallyIssuePlan")]
        public WmsResponseBody ExecuteManuallyIssuePlan([FromBody] PlanExecuteRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            //验证计划编号是否为空
            if (string.IsNullOrWhiteSpace(body.LocalPlanCode))
                throw new Exception("计划编号不能为空.");

            //执行计划
            CommonFunc.ExecuteManuallyIssuePlan(body.LocalPlanCode, body.UserID);

            return response;
        }

        /// <summary>
        /// 手工领料出库物料校验
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManuallyIssueMaterialCheck")]
        public WmsResponseBody ManuallyIssueMaterialCheck(string barcode)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    throw new Exception("物料条码不能为空.");
                }

                SqlParameter[] parameters =
                {
                    new SqlParameter("@F_MaterialBarcode", SqlDbType.NVarChar,255),
                    new SqlParameter("@message", SqlDbType.NVarChar, 500),
                    new SqlParameter("@return", SqlDbType.NVarChar, 50)
                };

                parameters[0].Value = CommonFunc.BarcodeProcess(barcode);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.ReturnValue;

                SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ManuallyIssueMaterialCheck", parameters);
                response.Code = parameters[2].Value.ToString();
                response.Msg = parameters[1].Value.ToString();
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 手工执行端子库和端子模具库生产领料计划
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ManuallyExecutePlan")]
        public WmsResponseBody ManuallyExecutePlan([FromBody] PlanExecuteRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();
            try
            {
                MutexSemaphore.WaitOne();

                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(body.LocalPlanCode))
                    {
                        throw new Exception("计划编号不能为空.");
                    }

                    //验证要执行的端子库发料计划
                    CommonFunc.ExecuteTerminalIssuePlanValidation(body.LocalPlanCode);

                    DataTable tablePlan = SqlDbHelper.GetDataSet($"select * from v_Project_PlanList where F_LocalPlanCode='{body.LocalPlanCode}' and F_AutoFlag=0").Tables[0]??
                        throw new Exception("获取计划明细信息失败，请检查网络链接或与开发人员联系");

                    if (tablePlan.Rows.Count <= 0)
                    {
                        throw new Exception("不存在要执行的计划单信息.");
                    }

                    //获取垂直升降库物料
                    DataRow[] rows0 = tablePlan.Select("F_StoreWarehouseID=5");

                    DataTable tableLiftMaterial = rows0.Length == 0 ?
                        CommonFunc.GetLiftInventoryDataTableScheme() :
                        CommonFunc.GetLiftInventoryMaterial(tablePlan.Rows[0]["F_RemotePlanCode"].ToString(), $"{body.UserCode}_{body.UserName}", rows0);

                    //执行计划
                    CommonFunc.ExecuteTerminalIssuePlan(body.LocalPlanCode, tableLiftMaterial, body.UserID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }
            finally
            {
                MutexSemaphore.Release();
            }
            return response;
        }

        /// <summary>
        /// 原料组箱操作
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialGroupBox")]
        public WmsResponseBody MaterialGroupBox([FromBody] MaterialGroupBoxRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();
            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证计划编号是否为空
                    if (string.IsNullOrWhiteSpace(body.LocalPlanCode))
                        throw new Exception("计划编号不能为空.");

                    //验证条码信息是否为空
                    if (body.Barcodes == null || body.Barcodes.Count <= 0)
                        throw new Exception("要组箱的物料条码信息不能为空.");

                    DataTable tableBarcodes = CommonFunc.GetGroupBoxBarcodeDataTableScheme();

                    foreach (var item in body.Barcodes)
                    { 
                        DataRow row = tableBarcodes.NewRow();
                        row["F_Barcode"] = CommonFunc.BarcodeProcess(item.Barcode);
                        tableBarcodes.Rows.Add(row);
                    }

                    //提交组箱操作信息
                    CommonFunc.SubmitMaterialGroupBox(body.LocalPlanCode, tableBarcodes, body.UserID);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }
            return response;
        }
        #endregion

        #region ---成品处理
        /// <summary>
        /// 获取载具信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBoxInfo")]
        public BoxQueryResponseBody GetBoxInfo()
        {
            BoxQueryResponseBody response = new BoxQueryResponseBody();
            try
            {
                DataTable table = SqlDbHelper.GetDataSet("select * from T_Base_Material where F_CategoryID=7 and F_IsEffective='use'").Tables[0] ??
                    throw new Exception("获取载具信息失败，请检查网络连接或与开发人员联系.");

                List<BoxBody> list = new List<BoxBody>();

                foreach (DataRow row in table.Rows)
                {
                    BoxBody box = new BoxBody
                    {
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        MaterialName = row["F_MaterialName"].ToString()
                    };
                    list.Add(box);
                }

                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }
        /// <summary>
        /// 获取发货库拣选位信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSortStation")]
        public StationQueryResponseBody GetSortStation()
        {
            StationQueryResponseBody response = new StationQueryResponseBody();
            try
            {
                DataTable table = SqlDbHelper.GetDataSet($"select F_ID,F_CellCode from T_Base_StoreCell where F_WarehouseID=4 and F_SortingFlag=1 and F_CellType='Cell'").Tables[0] ??
                        throw new Exception("获取拣选位信息失败，请检查网络连接或与开发人员联系.");

                List<StationBody> stations = new List<StationBody>();
                foreach (DataRow row in table.Rows)
                {
                    StationBody station = new StationBody
                    {
                        StationID = Convert.ToInt64(row["F_ID"]),
                        StationName = row["F_CellCode"].ToString()
                    };

                    stations.Add(station);
                }

                response.Data = stations;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }
        /// <summary>
        /// 载具入库申请
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BoxInbound")]
        public WmsResponseBody BoxInbound([FromBody] BoxInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    //验证给定的载具编码是否为空
                    if (string.IsNullOrWhiteSpace(body.MaterialCode))
                        throw new Exception("载具编码不能为空.");

                    //提交载具入库
                    CommonFunc.BoxInbound(body.StationID, body.MaterialCode);
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// 成品上架申请
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public WmsResponseBody ProductToShelvesReq([FromBody] ProductStoreToShelvesRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion
    }
}
