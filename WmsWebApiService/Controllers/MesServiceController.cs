using EliteHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// Mes相关服务接口
    /// </summary>
    [RoutePrefix("api/MesService")]
    public class MesServiceController : ApiController
    {
        private static Semaphore MutexSemaphore { get; set; }

        static MesServiceController()
        {
            //初始信号量值为1，最大信号量值为1  
            MutexSemaphore = new Semaphore(1, 1);
        }

        #region ---生产领料服务接口(自动+人工)
        /// <summary>
        /// MES请求原材料出库-自动出库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialReqOut")]
        public WmsResponseBody MaterialReqOut([FromBody] MesRequestOutboundTaskBody body)
        {
            WmsResponseBody response = new WmsResponseBody();
            string message = "";

            try
            {
                MutexSemaphore.WaitOne();

                if (body != null)
                {
                    if (string.IsNullOrWhiteSpace(body.StationCode))
                        throw new Exception("申请站台号不能为空.");

                    if (body.TaskList == null)
                        throw new Exception("申请任务明细信息不能为空.");

                    //获取MES申请物料信息
                    DataTable table = CommonFunc.GetRequestMaterialDataTableScheme();

                    //获取MES申请任务信息
                    DataTable tableTask = CommonFunc.GetRequestOutboundTaskDataTableScheme();

                    foreach (var task in body.TaskList)
                    {
                        if (string.IsNullOrWhiteSpace(task.LineCode))
                            throw new Exception("申请站台关联的输送线号不能为空.");

                        foreach (var material in task.MaterialList)
                        {
                            DataRow row = table.NewRow();
                            row["F_MaterialCode"] = material.MaterialCode;
                            row["F_MaterialName"] = material.MaterialName;
                            row["F_MaterialBarcode"] = material.MaterialBarcode;
                            row["F_Qty"] = material.Qty;
                            row["F_ConsumedQty"] = material.ConsumedQty;
                            row["F_EmptyFlag"] = material.EmptyFlag;
                            row["F_MaterialPS"] = material.MaterialPS;
                            table.Rows.Add(row);

                            DataRow dr = tableTask.NewRow();
                            dr["F_LineCode"] = task.LineCode;
                            dr["F_MaterialCode"] = material.MaterialCode;
                            dr["F_Qty"] = material.Qty;
                            tableTask.Rows.Add(dr);
                        }
                    }

                    if (table.Rows.Count == 0)
                        throw new Exception("物料信息不能为空.");

                    //获取物料明细信息
                    DataTable tableDetail = CommonFunc.GetLocalMaterialDetail(table);
                    if (tableDetail == null || tableDetail.Rows.Count <= 0)
                        throw new Exception("获取物料明细信息失败；请检查网络连接或与开发人员联系.");

                    //存在物料校验异常
                    DataRow[] rows0 = tableDetail.Select($"F_Succeed=0");
                    if (rows0.Length > 0)
                    {
                        StringBuilder fault = new StringBuilder();
                        foreach (DataRow row in rows0)
                        {
                            fault.Append($"[物料:{row["F_MaterialCode"]};异常:{row["F_Message"]}]\n");
                        }
                        throw new Exception(fault.ToString());
                    }

                    //存在垂直升降库物料，则校验库存数量
                    DataRow[] rows1 = tableDetail.Select($"F_WarehouseID=5");
                    if (rows1.Length > 0)
                    {
                        //生成领料计划
                        CommonFunc.CreateMaterialOutboundPlan(body.TaskCode, body.StationCode, "Unknow", tableTask);
                        //垂直升降库库存校验失败
                        if (!CommonFunc.LiftInventoryCheck(body.TaskCode, rows1, ref message))
                        {
                            CommonFunc.ChangeMaterialOutboundPlanStatus(body.TaskCode, 0);
                            throw new Exception(message);
                        }
                        else
                        {
                            CommonFunc.ChangeMaterialOutboundPlanStatus(body.TaskCode, 1);
                        }
                    }
                    else
                    {
                        //生成领料计划
                        CommonFunc.CreateMaterialOutboundPlan(body.TaskCode, body.StationCode, "Wait", tableTask);
                    }
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = body.TaskCode;
                response.Code = "300";
                response.Msg = ex.Message;
            }
            finally
            {
                MutexSemaphore.Release();
            }

            return response;
        }

        /// <summary>
        /// MES请求原材料出库-人工出库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialReqManuallyOut")]
        public WmsResponseBody MaterialReqManuallyOut([FromBody] MesRequestManuallyOutboundBody body)
        {
            WmsResponseBody response = new WmsResponseBody();
            string message = "";

            try
            {
                MutexSemaphore.WaitOne();

                if (body != null)
                {
                    if (string.IsNullOrWhiteSpace(body.StationCode))
                        throw new Exception("申请站台号不能为空.");

                    if (body.MaterialList == null)
                        throw new Exception("申请任务物料信息不能为空.");

                    //获取MES申请物料信息
                    DataTable table = CommonFunc.GetRequestMaterialDataTableScheme();

                    foreach (var material in body.MaterialList)
                    {
                        DataRow row = table.NewRow();
                        row["F_MaterialCode"] = material.MaterialCode;
                        row["F_MaterialName"] = "";
                        row["F_MaterialBarcode"] = "";
                        row["F_Qty"] = material.Qty;
                        row["F_ConsumedQty"] = 0;
                        row["F_EmptyFlag"] = "N";
                        row["F_MaterialPS"] = "";
                        table.Rows.Add(row);
                    }

                    if (table.Rows.Count == 0)
                        throw new Exception("物料信息不能为空.");

                    //获取物料明细信息
                    DataTable tableDetail = CommonFunc.GetLocalMaterialDetail(table);
                    if (tableDetail == null || tableDetail.Rows.Count <= 0)
                        throw new Exception("获取物料明细信息失败；请检查网络连接或与开发人员联系.");

                    //存在物料校验异常
                    DataRow[] rows0 = tableDetail.Select($"F_Succeed=0");
                    if (rows0.Length > 0)
                    {
                        StringBuilder fault = new StringBuilder();
                        foreach (DataRow row in rows0)
                        {
                            fault.Append($"[物料:{row["F_MaterialCode"]};异常:{row["F_Message"]}]\n");
                        }
                        throw new Exception(fault.ToString());
                    }
                    //垂直升降库要料信息
                    DataRow[] rows1 = tableDetail.Select($"F_WarehouseID=5");

                    //立体库要料信息
                    DataRow[] rows2 = tableDetail.Select($"F_WarehouseID in (1,2)");

                    //获取MES申请任务关联的立体库物料信息
                    DataTable tableTask = CommonFunc.GetRequestOutboundTaskDataTableScheme();
                    foreach (DataRow row in rows2)
                    {
                        DataRow dr = tableTask.NewRow();
                        dr["F_LineCode"] = "0";
                        dr["F_MaterialCode"] = row["F_MaterialCode"];
                        dr["F_Qty"] = row["F_Qty"];
                        tableTask.Rows.Add(dr);
                    }

                    //存在垂直升降库要料信息
                    if (rows1.Length > 0)
                    {
                        //存在立体库要料信息
                        if (tableTask.Rows.Count > 0)
                        {
                            //生成立体库手工要料计划；状态为“待定”
                            CommonFunc.CreateMaterialManuallyOutboundPlan(body.TaskCode, body.StationCode, "Unknow", tableTask);

                            //发送垂直升降库要料信息失败
                            if (!CommonFunc.SendMaterialManuallyOut(body.TaskCode, body.StationCode, rows1, ref message))
                            {
                                CommonFunc.ChangeMaterialOutboundPlanStatus(body.TaskCode, 0);
                                throw new Exception(message);
                            }
                            else
                            {
                                CommonFunc.ChangeMaterialOutboundPlanStatus(body.TaskCode, 1);
                            }
                        }
                        else
                        {
                            //发送垂直升降库要料信息失败
                            if (!CommonFunc.SendMaterialManuallyOut(body.TaskCode, body.StationCode, rows1, ref message))
                            {
                                throw new Exception(message);
                            }
                        }
                    }
                    else if (tableTask.Rows.Count > 0)
                    {
                        //生成立体库手工要料计划；状态为“等待”
                        CommonFunc.CreateMaterialManuallyOutboundPlan(body.TaskCode, body.StationCode, "Wait", tableTask);
                    }
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = body.TaskCode;
                response.Code = "300";
                response.Msg = ex.Message;
            }
            finally
            {
                MutexSemaphore.Release();
            }

            return response;
        }

        /// <summary>
        /// MES请求原材料出库计划查询
        /// </summary>
        /// <param name="planCode">要查询的计划号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("IssuePlanQuery")]
        public MesIssuePlanQuery IssuePlanQuery(string planCode)
        {
            MesIssuePlanQuery response = new MesIssuePlanQuery
            {
                PlanCode = planCode
            };

            try
            {
                if (string.IsNullOrWhiteSpace(planCode))
                    throw new Exception("要查询的计划号不能为空.");

                DataTable tablePlan = SqlDbHelper.GetDataSet($"select * from v_Project_Plan where F_RemotePlanCode='{planCode}'").Tables[0] ??
                    throw new Exception("获取计划信息失败，请检查网络连接或与开发人员联系.");

                if (tablePlan.Rows.Count == 0)
                    throw new Exception("要查询的计划信息不存在.");

                string planType = tablePlan.Rows[0]["F_PlanType"].ToString().Trim();
                if (planType != "101" && planType != "103")
                    throw new Exception($"{planCode} 不属于领料计划，无法查询库存物料分配信息.");

                SqlParameter[] parameters =
                {
                    new SqlParameter("@F_PlanCode",SqlDbType.NVarChar, 255)
                };

                parameters[0].Value = planCode;

                DataTable tablePlanList = SqlDbHelper.GetDataSet("Elite_P_Project_IssuePlanInventoryAllocationQuery", parameters).Tables[0] ??
                    throw new Exception("获取计划信息失败，请检查网络连接或与开发人员联系.");

                List<IssuePlanList> list = new List<IssuePlanList>();

                foreach(DataRow row in tablePlanList.Rows)
                {
                    IssuePlanList planList = new IssuePlanList
                    {
                        MaterialCode = row["F_MaterialCode"].ToString(),
                        IssueQty = Convert.ToDecimal(row["F_IssueQty"])
                    };

                    list.Add(planList);
                }

                response.PlanList = list;
            }
            catch(Exception ex)
            {
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion

        #region ---生产退料服务接口
        /// <summary>
        /// MES请求原材料返库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MaterialReqReturn")]
        public WmsResponseBody MaterialReqReturn([FromBody] MesRequestReturnTaskBody body)
        {
            WmsResponseBody response = new WmsResponseBody();
            try
            {
                if (body != null)
                {
                    if (string.IsNullOrWhiteSpace(body.StationCode))
                        throw new Exception("申请站台号不能为空.");

                    if (string.IsNullOrWhiteSpace(body.LineCode))
                        throw new Exception("申请站台关联的输送线号不能为空.");

                    if (body.MaterialList == null)
                        throw new Exception("物料信息不能为空.");

                    //获取MES申请物料信息
                    DataTable table = CommonFunc.GetRequestMaterialDataTableScheme();
                    foreach (var material in body.MaterialList)
                    {
                        DataRow row = table.NewRow();
                        row["F_MaterialCode"] = material.MaterialCode;
                        row["F_MaterialName"] = material.MaterialName;
                        row["F_MaterialBarcode"] = CommonFunc.BarcodeProcess(material.MaterialBarcode);
                        row["F_Qty"] = material.Qty;
                        row["F_ConsumedQty"] = material.ConsumedQty;
                        row["F_EmptyFlag"] = material.EmptyFlag;
                        row["F_MaterialPS"] = material.MaterialPS;
                        table.Rows.Add(row);
                    }

                    if (table.Rows.Count == 0)
                        throw new Exception("物料信息不能为空.");

                    //生成退料计划
                    CommonFunc.CreateMaterialReturnPlan(body.TaskCode, body.StationCode, body.LineCode, table);
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = body.TaskCode;
                response.Code = "300";
                response.Msg = ex.Message;
            }
            return response;
        }
        #endregion

        #region ---机台旋转到位通知服务接口
        /// <summary>
        /// MES发送机台旋转通知
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StationRotation")]
        public WmsResponseBody StationRotation([FromBody] MesStationRotationReportBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body != null)
                {
                    if (body.StationList != null)
                    {
                        //获取机台信息表结构
                        DataTable tableMachine = CommonFunc.GetMachineDataTableScheme();
                        foreach (var material in body.StationList)
                        {
                            DataRow row = tableMachine.NewRow();
                            row["F_StationCode"] = material.StationCode;
                            row["F_LineCode"] = material.LineCode;
                            tableMachine.Rows.Add(row);
                        }

                        if (tableMachine.Rows.Count > 0)
                        {
                            CommonFunc.SyncMachineRotateStatus(tableMachine);
                        }
                    }
                }
                else
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion

        #region ---更改库存服务接口
        /// <summary>
        /// MES发送机台旋转通知
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeInventory")]
        public WmsResponseBody ChangeInventory([FromBody] MesRequestChangeInventoryBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body != null)
                {
                    if (body.MaterialList != null)
                    {
                        //获取机台信息表结构
                        DataTable table = CommonFunc.GetChangeInventoryDataTableScheme();
                        foreach (var inventory in body.MaterialList)
                        {
                            DataRow row = table.NewRow();
                            row["F_MaterialBarcode"] = inventory.MaterialBarcode;
                            row["F_Qty"] = inventory.Qty;
                            table.Rows.Add(row);
                        }

                        if (table.Rows.Count > 0)
                        {
                            CommonFunc.ChangeInventoryReq(table);
                        }
                    }
                }
                else
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion

        #region ---成品完工申请入库
        /// <summary>
        /// MES请求成品完工入库
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinishedProductReqIn")]
        public WmsResponseBody FinishedProductReqIn([FromBody] ProductInboundRequestBody body)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                if (body != null)
                {
                    if (string.IsNullOrWhiteSpace(body.StationCode))
                        throw new Exception("申请站台号不能为空.");

                    if (body.MaterialList == null)
                        throw new Exception("物料信息不能为空.");

                    if (string.IsNullOrWhiteSpace(body.MaterialPS))
                        throw new Exception("成品包装规格不能为空.");

                    //获取MES申请成品入库信息
                    DataTable table = CommonFunc.GetProductInboundDataTableScheme();
                    foreach (var material in body.MaterialList)
                    {
                        DataRow row = table.NewRow();
                        row["F_MaterialCode"] = material.MaterialCode;
                        row["F_MaterialBarcode"] = CommonFunc.BarcodeProcess(material.MaterialBarcode);
                        row["F_Qty"] = material.Qty;
                        row["F_BatchNo"] = material.BatchNo;
                        table.Rows.Add(row);
                    }

                    if (table.Rows.Count == 0)
                        throw new Exception("成品入库信息不能为空.");

                    //生成成品完工入库计划
                    CommonFunc.CreateProductInboundPlan(body.TaskCode, body.StationCode, body.MaterialPS, table);
                }
                else
                {
                    response.TaskCode = "";
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.TaskCode = body.TaskCode;
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
        #endregion
    }
}
