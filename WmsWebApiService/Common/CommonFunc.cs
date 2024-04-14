using EliteHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 通用服务类
    /// </summary>
    public static class CommonFunc
    {
        #region ---变量定义
        private static readonly string[] WcsNormalReportType = { "TASK_REPORT", "BARCODE", "PALLET_HEIGHT", "PALLET_WEIGHT", "INBOUND_APPLY", "VISION_CHECK", "PALLET_SIZE_ERROR", "AGV_GET_SUCCESS" };
        private static readonly string[] WcsSignalReportType = { "SIGNAL_REPORT" };
        private static readonly AppConfig appConfig = new AppConfig();
        #endregion

        #region ---公用方法
        private static bool FindField(string[] source, string fieldName)
        {
            bool result = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].Equals(fieldName))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_32(string input)
        {
            MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 物料条码处理，去掉开头的3S
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string BarcodeProcess(string barcode)
        {
            return barcode.Substring(0, 2).ToUpper().Equals("3S") ? barcode.Substring(2) : barcode;
        }

        /// <summary>
        /// 获取时间戳(长度为13位)
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp13()
        {
            /*
            //ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);

            //得到精确到毫秒的时间戳（长度13位）
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
            */

            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
        }
        /// <summary>
        /// 获取时间戳(长度为10位)
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp10()
        {
            /*
            //ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            //得到精确到毫秒的时间戳（长度10位）
            return Convert.ToInt64(ts.TotalSeconds).ToString();
            */

            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        }
        #endregion

        #region ---数据结构
        /// <summary>
        /// 获取更改库存表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetChangeInventoryDataTableScheme()
        {
            return new DataTable("T_ChangeInventory")
            {
                Columns =
                {
                    new DataColumn("F_MaterialBarcode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal"))
                }
            };
        }
        /// <summary>
        /// 获取成品备货表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPrepareProductDataTableScheme()
        {
            return new DataTable("T_Prepare")
            {
                Columns =
                {
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal"))
                }
            };
        }
        /// <summary>
        /// 获取垂直升降库库存表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLiftInventoryDataTableScheme()
        {
            return new DataTable("T_LiftInventory")
            {
                Columns =
                {
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialName", Type.GetType("System.String")),
                    new DataColumn("F_MaterialBarcode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal")),
                    new DataColumn("F_BatchNo", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// MES申请物料信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRequestMaterialDataTableScheme()
        {
            return new DataTable("T_RequestMaterial")
            {
                Columns =
                {
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialName", Type.GetType("System.String")),
                    new DataColumn("F_MaterialBarcode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal")),
                    new DataColumn("F_ConsumedQty", Type.GetType("System.Decimal")),
                    new DataColumn("F_EmptyFlag", Type.GetType("System.String")),
                    new DataColumn("F_MaterialPS", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// 获取成品入库表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductInboundDataTableScheme()
        {
            return new DataTable("T_ProductInbound")
            {
                Columns =
                {
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialBarcode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal")),
                    new DataColumn("F_BatchNo", Type.GetType("System.String"))
                }
            };
        }

        /// <summary>
        /// MES请求出库任务明细信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRequestOutboundTaskDataTableScheme()
        {
            return new DataTable("T_RequestOutTaskDetail")
            {
                Columns =
                {
                    new DataColumn("F_LineCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal"))
                }
            };
        }

        /// <summary>
        /// 标签信息表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBarcodeDetailDataTableScheme()
        {
            return new DataTable("T_BarcodeDetail")
            {
                Columns =
                {
                    new DataColumn("Id", Type.GetType("System.String")),
                    new DataColumn("LabelNo", Type.GetType("System.String")),
                    new DataColumn("ItemCode", Type.GetType("System.String")),
                    new DataColumn("ItemName", Type.GetType("System.String")),
                    new DataColumn("Qty", Type.GetType("System.Decimal")),
                    new DataColumn("Unit", Type.GetType("System.String")),
                    new DataColumn("Location", Type.GetType("System.String")),
                    new DataColumn("FlabelNo", Type.GetType("System.String")),
                    new DataColumn("LabelType", Type.GetType("System.String")),
                    new DataColumn("BatchNo", Type.GetType("System.String")),
                    new DataColumn("ActiveStatus", Type.GetType("System.String")),
                    new DataColumn("LabelPrintDate", Type.GetType("System.String"))
                }
            };
        }

        /// <summary>
        /// 获取机台信息表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMachineDataTableScheme()
        {
            return new DataTable("T_Machine")
            {
                Columns =
                {
                    new DataColumn("F_StationCode", Type.GetType("System.String")),
                    new DataColumn("F_LineCode", Type.GetType("System.Int32"))
                }
            };
        }

        /// <summary>
        /// 获取组箱条码表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGroupBoxBarcodeDataTableScheme()
        {
            return new DataTable("T_Barcode")
            {
                Columns =
                {
                    new DataColumn("F_Barcode", Type.GetType("System.String"))
                }
            };
        }

        /// <summary>
        /// WCS上报信息表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWcsReportDataTableScheme()
        {
            return new DataTable("T_WcsReport")
            {
                Columns =
                {
                    new DataColumn("F_ReportType", Type.GetType("System.String")),
                    new DataColumn("F_WmsTaskID", Type.GetType("System.String")),
                    new DataColumn("F_WcsTaskID", Type.GetType("System.String")),
                    new DataColumn("F_ErrorMsg", Type.GetType("System.String")),
                    new DataColumn("F_StationID", Type.GetType("System.String")),
                    new DataColumn("F_ReportVal", Type.GetType("System.String")),
                    new DataColumn("F_ReportTime", Type.GetType("System.String"))
                }
            };
        }

        #endregion

        #region ---Mes服务相关
        /// <summary>
        /// 获取Mes申请物料的明细信息
        /// </summary>
        /// <param name="tableReqMaterial">Mes申请物料</param>
        /// <returns></returns>
        public static DataTable GetLocalMaterialDetail(DataTable tableReqMaterial)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@material",SqlDbType.Structured)
            };
            parameters[0].Value = tableReqMaterial;

            return SqlDbHelper.GetDataSet("Elite_P_System_MaterialQuery", parameters).Tables[0];
        }

        /// <summary>
        /// 获取给定物料条码的生产消耗数
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static decimal GetMaterialConsumedQty(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                throw new Exception("物料条码不能为空.");
            QueryMaterialBody query = new QueryMaterialBody
            {
                QRCord = barcode
            };

            QueryMesMaterialRequestBody request = new QueryMesMaterialRequestBody
            {
                DATA = query
            };

            string json = JsonHelper.ObjectToJson(request);
            //记录日志
            LoggerHelper.LogWms(json);
            var result = CustomHttpClient.PostDataToMes(appConfig.GetKeyValue("MesServiceUrl"), json);

            //记录日志；mes返回信息
            LoggerHelper.LogWms($"物料 {barcode} 消耗数查询返回信息: {result}");

            if (string.IsNullOrWhiteSpace(result))
                throw new Exception($"物料 {barcode} 消耗数查询失败；返回信息为空.");

            var response = JsonHelper.JsonToObject<MesResponseBody>(result);
            if (!response.STATUSCODE.Equals("200"))
                throw new Exception($"物料 {barcode} 消耗数查询失败；{response.MSG}");

            return response.DATA.rel;
        }
        #endregion

        #region ---Wcs服务相关
        /// <summary>
        /// 上报类型验证
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static bool ValidateReportType(string reportType, string serviceType)
        {
            return serviceType.ToLower().Equals("signal") ? FindField(WcsSignalReportType, reportType) : FindField(WcsNormalReportType, reportType);
        }
        #endregion

        #region ---厂级WMS相关
        /// <summary>
        /// 获取厂级WMS接口访问Token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFactoryWmsToken()
        {
            FactoryWmsTokenInParamsBody body = new FactoryWmsTokenInParamsBody
            {
                User_code = appConfig.GetKeyValue("user_code"),
                appid = appConfig.GetKeyValue("appid"),
                Appsecret = appConfig.GetKeyValue("appsecret")
            };

            string json = JsonHelper.ObjectToJson(body);

            //记录日志；wms发送信息
            LoggerHelper.LogFactory($"获取厂级WMS接口访问Token；{json}.");

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "funid", body.Funid },
                { "eventcode", body.Eventcode },
                { "user_code", body.User_code },
                { "appid", body.appid },
                { "appsecret", body.Appsecret }
            };

            var result = CustomHttpClient.PostKeyValuePair(appConfig.GetKeyValue("factoryWmsUrl"), dic);

            //记录返回的信息
            LoggerHelper.LogFactory(result);

            if (string.IsNullOrWhiteSpace(result))
                throw new Exception("获取厂级WMS接口访问Token失败；返回信息为空.");

            var response = JsonHelper.JsonToObject<FactoryWmsTokenOutParamsBody>(result);
            if (!response.Success)
                throw new Exception($"获取厂级WMS接口访问Token失败；{response.Message}");

            return response.Data.Access_token;
        }

        /// <summary>
        /// 获取标签信息
        /// </summary>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public static DataTable GetBarcodeDetail(List<string> barcodes)
        {
            if (barcodes.Count <= 0)
                throw new Exception("物料条码不能为空");

            StringBuilder sb = new StringBuilder();
            foreach (var barcode in barcodes)
            {
                sb.Append($"{barcode},");
            }
            string query = sb.ToString();
            FactoryWmsBarcodeQueryInParamsBody body = new FactoryWmsBarcodeQueryInParamsBody
            {
                Access_token = GetFactoryWmsToken(),
                Labelno = query.Substring(0, query.Length - 1)
            };

            string json = JsonHelper.ObjectToJson(body);

            //记录日志；wms发送信息
            LoggerHelper.LogFactory($"获取标签信息；{json}.");

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "funid", body.Funid },
                { "eventcode", body.Eventcode },
                { "user_code", body.User_code },
                { "access_token", body.Access_token },
                { "labelno", body.Labelno }
            };

            var result = CustomHttpClient.PostKeyValuePair(appConfig.GetKeyValue("factoryWmsUrl"), dic);

            //记录返回的信息
            LoggerHelper.LogFactory(result);

            if (string.IsNullOrWhiteSpace(result))
                throw new Exception("获取标签信息失败；返回信息为空.");

            var response = JsonHelper.JsonToObject<FactoryWmsBarcodeQueryOutParamsBody>(result);
            if (!response.Success)
                throw new Exception($"获取标签信息失败；{response.Message}");

            DataTable table = GetBarcodeDetailDataTableScheme();
            foreach(var item in response.Data)
            {
                DataRow row = table.NewRow();
                row["Id"] = item.Id;
                row["LabelNo"] = item.Labelno;
                row["ItemCode"] = item.Itemcode;
                row["ItemName"] = item.Itemname;
                row["Qty"] = item.Qty;
                row["Unit"] = item.Unit;
                row["Location"] = item.Location;
                row["FlabelNo"] = item.Flabelno;
                row["LabelType"] = item.Labeltype;
                row["BatchNo"] = item.Batchno;
                row["ActiveStatus"] = item.Activestatus;
                row["LabelPrintDate"] = item.Labelprintdate;
                table.Rows.Add(row);
            }

            return table;
        }
        #endregion

        #region ---垂直升降库Wms服务相关
        /// <summary>
        /// 垂直升降库库存物料数量校验
        /// </summary>
        /// <param name="mesTaskCode">mes申请任务号</param>
        /// <param name="materials"></param>
        /// <param name="message"></param>
        public static bool LiftInventoryCheck(string mesTaskCode, DataRow[] materials, ref string message)
        { 
            if (materials.Length>0)
            {
                LiftInventoryCheckBody checkBody = new LiftInventoryCheckBody
                {
                    TaskCode = mesTaskCode,
                    ReqTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                List<LiftInventoryCheckMaterialBody> materialList = new List<LiftInventoryCheckMaterialBody>();
                foreach (var material in materials)
                {
                    LiftInventoryCheckMaterialBody body = new LiftInventoryCheckMaterialBody
                    {
                        MaterialCode = material["F_MaterialCode"].ToString(),
                        MaterialName = material["F_MaterialName"].ToString(),
                        Qty = Convert.ToDecimal(material["F_Qty"]) + Convert.ToDecimal(material["F_Qty"]) * Convert.ToInt32(material["F_Ratio"]) / 100,
                        AllocatedQty = Convert.ToDecimal(material["F_AllocatedQty"])
                    };
                    materialList.Add(body);
                }

                checkBody.MaterialList = materialList;

                string json = JsonHelper.ObjectToJson(checkBody);

                //记录日志；wms发送信息 
                LoggerHelper.LogLift($"垂直升降库库存校验；{json}.");

                var result = CustomHttpClient.PostData($"{appConfig.GetKeyValue("liftWmsUrl")}/api/StockVerify", json);

                //记录返回的信息
                LoggerHelper.LogLift(result);

                if (string.IsNullOrWhiteSpace(result))
                {
                    message = "垂直升降库库存校验失败；返回信息为空.";
                    return false;
                }

                var response = JsonHelper.JsonToObject<WmsResponseBody>(result);

                if (!response.Code.Equals("200"))
                {
                    message = $"垂直升降库库存校验失败；{response.Msg}";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取垂直升降库出库物料明细信息
        /// </summary>
        /// <param name="mesTaskCode"></param>
        /// <param name="userName"></param>
        /// <param name="materials"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DataTable GetLiftInventoryMaterial(string mesTaskCode, string userName, DataRow[] materials)
        {
            LiftMaterialOutboundRequestBody requestBody = new LiftMaterialOutboundRequestBody
            {
                TaskCode = mesTaskCode,
                ReqTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UserName = userName
            };

            List<LiftMaterialBody> list = new List<LiftMaterialBody>();
            foreach (DataRow row in materials)
            {
                LiftMaterialBody materialBody = new LiftMaterialBody
                {
                    MaterialCode = row["F_MaterialCode"].ToString(),
                    MaterialName = row["F_MaterialName"].ToString(),
                    Qty = Convert.ToDecimal(row["F_Qty"])
                };

                list.Add(materialBody);
            }

            requestBody.MaterialList = list;

            string json = JsonHelper.ObjectToJson(requestBody);

            //记录日志；wms发送信息 
            LoggerHelper.LogLift($"垂直升降库物料出库申请；{json}.");

            var result = CustomHttpClient.PostData($"{appConfig.GetKeyValue("liftWmsUrl")}/api/ApplyGoodsOut", json);

            //记录返回的信息
            LoggerHelper.LogLift(result);

            if (string.IsNullOrWhiteSpace(result))
                throw new Exception("获取垂直升降库出库物料信息失败；返回信息为空.");

            var response = JsonHelper.JsonToObject<LiftMaterialOutboundResponseBody>(result);
            if (!response.Code.Equals("200"))
                throw new Exception($"获取垂直升降库出库物料信息失败；{response.Msg}");

            DataTable table = GetLiftInventoryDataTableScheme();
            foreach(var item in response.MaterialList)
            {
                if (string.IsNullOrWhiteSpace(item.MaterialBarcode))
                    throw new Exception($"物料: {item.MaterialCode} 的物料条码为空.");
                DataRow row = table.NewRow();
                row["F_MaterialCode"] = item.MaterialCode;
                row["F_MaterialName"] = item.MaterialName;
                row["F_MaterialBarcode"] = item.MaterialBarcode;
                row["F_Qty"] = item.Qty;
                row["F_BatchNo"] = item.BatchNo;
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 将MES手工要料信息发送给垂直升降库
        /// </summary>
        /// <param name="mesTaskCode"></param>
        /// <param name="stationCode"></param>
        /// <param name="materials"></param>
        /// <param name="message"></param>
        public static bool SendMaterialManuallyOut(string mesTaskCode, string stationCode, DataRow[] materials, ref string message)
        {
            //物料信息
            List<ManuallyOutboundMaterialBody> materialList = new List<ManuallyOutboundMaterialBody>();

            foreach (DataRow row in materials)
            {
                ManuallyOutboundMaterialBody material = new ManuallyOutboundMaterialBody
                {
                    MaterialCode = row["F_MaterialCode"].ToString(),
                    Qty = Convert.ToDecimal(row["F_Qty"])
                };
                materialList.Add(material);
            }

            //请求信息
            MesRequestManuallyOutboundBody body = new MesRequestManuallyOutboundBody
            {
                TaskCode = mesTaskCode,
                StationCode = stationCode,
                ReqTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                MaterialList = materialList
            };

            string json = JsonHelper.ObjectToJson(body);

            //记录日志；wms发送信息 
            LoggerHelper.LogLift($"发送垂直升降库物料手工出库信息；{json}.");

            var result = CustomHttpClient.PostData($"{appConfig.GetKeyValue("liftWmsUrl")}/api/ManualPickInfor", json);

            //记录返回的信息
            LoggerHelper.LogLift(result);

            if (string.IsNullOrWhiteSpace(result))
            {
                message = "发送垂直升降库物料手工出库信息失败；返回信息为空.";
                return false;
            }

            var response = JsonHelper.JsonToObject<WmsResponseBody>(result);
            if (!response.Code.Equals("200"))
            {
                message = $"发送垂直升降库物料手工出库信息失败；{response.Msg}";
                return false;
            }

            return true;
        }
        #endregion

        #region ---立体库WMS相关
        /// <summary>
        /// 原材料入库：导线为新品入库；端子为新品或返库
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="station"></param>
        /// <param name="materialBarcode"></param>
        /// <param name="materialCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="qty"></param>
        /// <param name="operatorId"></param>
        /// <exception cref="Exception"></exception>
        public static void MaterialInbound(long warehouse, long station, string materialBarcode, string materialCode, string batchNo, decimal qty, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_WarehouseID", SqlDbType.BigInt),
                new SqlParameter("@F_StationID", SqlDbType.BigInt),
                new SqlParameter("@F_MaterialBarcode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_MaterialCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_BatchNo", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_Qty", SqlDbType.Decimal),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = warehouse;
            parameters[1].Value = station;
            parameters[2].Value = materialBarcode;
            parameters[3].Value = materialCode;
            parameters[4].Value = batchNo;
            parameters[5].Value = qty;
            parameters[6].Value = operatorId;
            parameters[7].Direction = ParameterDirection.Output;
            parameters[8].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_System_MaterialInbound", parameters);
            if (!parameters[8].Value.ToString().Equals("200"))
                throw new Exception(parameters[7].Value.ToString());
        }

        /// <summary>
        /// 原材料盘点入库
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="station"></param>
        /// <param name="materialBarcode"></param>
        /// <param name="operatorId"></param>
        /// <exception cref="Exception"></exception>
        public static void MaterialCheckInbound(long warehouse, long station, string materialBarcode, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_WarehouseID", SqlDbType.BigInt),
                new SqlParameter("@F_StationID", SqlDbType.BigInt),
                new SqlParameter("@F_MaterialBarcode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = warehouse;
            parameters[1].Value = station;
            parameters[2].Value = materialBarcode;
            parameters[3].Value = operatorId;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_System_MaterialCheckInbound", parameters);
            if (!parameters[5].Value.ToString().Equals("200"))
                throw new Exception(parameters[4].Value.ToString());
        }

        /// <summary>
        /// 原材料退料入库
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="station"></param>
        /// <param name="materialBarcode"></param>
        /// <param name="consumedQty"></param>
        /// <param name="operatorId"></param>
        public static void MaterialReturnInbound(long warehouse, long station, string materialBarcode, decimal consumedQty, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_WarehouseID", SqlDbType.BigInt),
                new SqlParameter("@F_StationID", SqlDbType.BigInt),
                new SqlParameter("@F_MaterialBarcode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_ConsumedQty", SqlDbType.Decimal),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = warehouse;
            parameters[1].Value = station;
            parameters[2].Value = materialBarcode;
            parameters[3].Value = consumedQty;
            parameters[4].Value = operatorId;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_System_MaterialReturnInbound", parameters);
            if (!parameters[6].Value.ToString().Equals("200"))
                throw new Exception(parameters[5].Value.ToString());
        }

        /// <summary>
        /// 空轴处理
        /// </summary>
        /// <param name="materialBarcode"></param>
        /// <param name="consumedQty"></param>
        /// <param name="operatorId"></param>
        public static void ProcessEmptyMaterial(string materialBarcode, decimal consumedQty, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_MaterialBarcode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_ConsumedQty", SqlDbType.Decimal),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = materialBarcode;
            parameters[1].Value = consumedQty;
            parameters[2].Value = operatorId;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_System_ProcessEmptyMaterial", parameters);
            if (!parameters[4].Value.ToString().Equals("200"))
                throw new Exception(parameters[3].Value.ToString());
        }
        /// <summary>
        /// 验证要执行的端子、小盘端子和模具生产领料计划
        /// </summary>
        /// <param name="planCode"></param>
        public static void ExecuteTerminalIssuePlanValidation(string planCode)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_LocalPlanCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = planCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ExecuteTerminalIssuePlanValidation", parameters);
            if (!parameters[2].Value.ToString().Equals("200"))
                throw new Exception(parameters[1].Value.ToString());
        }

        /// <summary>
        /// 执行端子、小盘端子和模具生产领料计划
        /// </summary>
        /// <param name="planCode"></param>
        /// <param name="tableLiftMaterial"></param>
        /// <param name="operatorId"></param>
        public static void ExecuteTerminalIssuePlan(string planCode, DataTable tableLiftMaterial, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_LocalPlanCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@liftMaterial", SqlDbType.Structured),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = planCode;
            parameters[1].Value = tableLiftMaterial;
            parameters[2].Value = operatorId;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ExecuteTerminalIssuePlan", parameters);
            if (!parameters[4].Value.ToString().Equals("200"))
                throw new Exception(parameters[3].Value.ToString());
        }

        /// <summary>
        /// 执行手工领料单
        /// </summary>
        /// <param name="planCode"></param>
        /// <param name="operatorId"></param>
        public static void ExecuteManuallyIssuePlan(string planCode, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_LocalPlanCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = planCode;
            parameters[1].Value = operatorId;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ExecuteManuallyIssuePlan", parameters);
            if (!parameters[3].Value.ToString().Equals("200"))
                throw new Exception(parameters[2].Value.ToString());
        }

        /// <summary>
        /// 提交原料组箱操作
        /// </summary>
        /// <param name="planCode"></param>
        /// <param name="tableBarcodes"></param>
        /// <param name="operatorId"></param>
        /// <exception cref="Exception"></exception>
        public static void SubmitMaterialGroupBox(string planCode, DataTable tableBarcodes, long operatorId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_LocalPlanCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@barcode", SqlDbType.Structured),
                new SqlParameter("@F_Operator", SqlDbType.BigInt),
                new SqlParameter("@message", SqlDbType.NVarChar, 500),
                new SqlParameter("@return", SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = planCode;
            parameters[1].Value = tableBarcodes;
            parameters[2].Value = operatorId;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_MaterialGroupBox", parameters);
            if (!parameters[4].Value.ToString().Equals("200"))
                throw new Exception(parameters[3].Value.ToString());
        }

        /// <summary>
        /// 更新领料计划状态；删除或更新为等待执行
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="status"></param>
        public static void ChangeMaterialOutboundPlanStatus(string taskCode, int status)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@status", SqlDbType.Int)
            };
            parameters[0].Value = taskCode;
            parameters[1].Value = status;
            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ChangeMaterialOutboundPlanStatus", parameters);
        }

        /// <summary>
        /// 创建自动送料计划
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="stationCode"></param>
        /// <param name="planStatus"></param>
        /// <param name="tableTask"></param>
        public static void CreateMaterialOutboundPlan(string taskCode, string stationCode, string planStatus, DataTable tableTask)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@stationCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@planStatus", SqlDbType.NVarChar, 255),
                new SqlParameter("@material", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };
            parameters[0].Value = taskCode;
            parameters[1].Value = stationCode;
            parameters[2].Value = planStatus;
            parameters[3].Value = tableTask;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_CreateMaterialOutboundPlan", parameters);

            if (!parameters[5].Value.ToString().Equals("200"))
                throw new Exception(parameters[4].Value.ToString());
        }
        /// <summary>
        /// 创建手工送料计划
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="stationCode"></param>
        /// <param name="planStatus"></param>
        /// <param name="tableTask"></param>
        public static void CreateMaterialManuallyOutboundPlan(string taskCode, string stationCode, string planStatus, DataTable tableTask)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@stationCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@planStatus", SqlDbType.NVarChar, 255),
                new SqlParameter("@material", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = taskCode;
            parameters[1].Value = stationCode;
            parameters[2].Value = planStatus;
            parameters[3].Value = tableTask;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_CreateMaterialManuallyOutboundPlan", parameters);

            if (!parameters[5].Value.ToString().Equals("200"))
                throw new Exception(parameters[4].Value.ToString());
        }
        /// <summary>
        /// 创建成品完工入库计划
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="stationCode"></param>
        /// <param name="packingCategoryCode"></param>
        /// <param name="tableProduct"></param>
        public static void CreateProductInboundPlan(string taskCode, string stationCode, string packingCategoryCode, DataTable tableProduct)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@stationCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@packingCategoryCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@product", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = taskCode;
            parameters[1].Value = stationCode;
            parameters[2].Value = packingCategoryCode;
            parameters[3].Value = tableProduct;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_CreateProductInboundPlan", parameters);

            if (!parameters[5].Value.ToString().Equals("200"))
                throw new Exception(parameters[4].Value.ToString());
        }
        /// <summary>
        /// 创建成品备货计划
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="tableProduct"></param>
        public static void CreateProductPreparePlan(string taskCode, DataTable tableProduct)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@product", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = taskCode;
            parameters[1].Value = tableProduct;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_CreateProductOutboundPlan", parameters);

            if (!parameters[3].Value.ToString().Equals("200"))
                throw new Exception(parameters[2].Value.ToString());
        }
        /// <summary>
        /// 生成机台退料计划
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="stationCode"></param>
        /// <param name="lineCode"></param>
        /// <param name="tableTask"></param>
        public static void CreateMaterialReturnPlan(string taskCode, string stationCode, string lineCode, DataTable tableTask)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@taskCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@stationCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@lineCode", SqlDbType.NVarChar, 255),
                new SqlParameter("@material", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = taskCode;
            parameters[1].Value = stationCode;
            parameters[2].Value = lineCode;
            parameters[3].Value = tableTask;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_CreateMaterialReturnPlan", parameters);

            if (!parameters[5].Value.ToString().Equals("200"))
                throw new Exception(parameters[4].Value.ToString());
        }
        /// <summary>
        /// 同步机台旋转状态
        /// </summary>
        /// <param name="tableMachine"></param>
        public static void SyncMachineRotateStatus(DataTable tableMachine)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@machine", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = tableMachine;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_SyncMachineRotateStatus", parameters);

            if (!parameters[2].Value.ToString().Equals("200"))
                throw new Exception(parameters[1].Value.ToString());
        }
        /// <summary>
        /// 更改库存数量
        /// </summary>
        /// <param name="tableInventory"></param>
        public static void ChangeInventoryReq(DataTable tableInventory)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@material", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = tableInventory;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_ChangeInventoryReq", parameters);

            if (!parameters[2].Value.ToString().Equals("200"))
                throw new Exception(parameters[1].Value.ToString());
        }
        /// <summary>
        /// 提交成品上架存储或直接出库
        /// </summary>
        /// <param name="tableProduct"></param>
        /// <param name="type">类型；Up：上架 Out：出库</param>
        public static void SubmitProductShelvesOrOut(DataTable tableProduct, string type)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@product", SqlDbType.Structured),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = tableProduct;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            string storedProcedureName = type.Equals("up") ? "Elite_P_Project_ExecuteProductStoreToShelves" : "Elite_P_Project_ExecuteProductOutbound";

            SqlDbHelper.ExecuteNonQuery(storedProcedureName, parameters);

            if (!parameters[2].Value.ToString().Equals("200"))
                throw new Exception(parameters[1].Value.ToString());
        }
        /// <summary>
        /// 提交载具入库
        /// </summary>
        /// <param name="station"></param>
        /// <param name="materialCode"></param>
        public static void BoxInbound(long station, string materialCode)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@F_StationID", SqlDbType.BigInt),
                new SqlParameter("@F_MaterialCode",SqlDbType.NVarChar, 255),
                new SqlParameter("@message",SqlDbType.NVarChar, 500),
                new SqlParameter("@return",SqlDbType.NVarChar, 50)
            };

            parameters[0].Value = station;
            parameters[1].Value = materialCode;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.ReturnValue;

            SqlDbHelper.ExecuteNonQuery("Elite_P_Project_BoxInbound", parameters);

            if (!parameters[3].Value.ToString().Equals("200"))
                throw new Exception(parameters[2].Value.ToString());
        }
        #endregion
    }
}