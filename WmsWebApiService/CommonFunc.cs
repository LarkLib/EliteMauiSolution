using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 通用服务类
    /// </summary>
    public static class CommonFunc
    {
        private static readonly string[] WcsNormalReportType = { "TASK_REPORT", "BARCODE", "PALLET_HEIGHT", "PALLET_WEIGHT", "INBOUND_APPLY", "VISION_CHECK", "PALLET_SIZE_ERROR" };
        private static readonly string[] WcsSignalReportType = { "SIGNAL_REPORT", "SIGNAL_QUERY" };

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

        #region ---数据结构
        /// <summary>
        /// SCM入库申请主表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInboundPlanDataTableScheme()
        {
            return new DataTable("T_ScmInboundPlanMain")
            {
                Columns =
                {
                    new DataColumn("F_RemotePlanCode", Type.GetType("System.String")),
                    new DataColumn("F_PlanType", Type.GetType("System.String")),
                    new DataColumn("F_OperType", Type.GetType("System.String")),
                    new DataColumn("F_ModifyQtyFlag", Type.GetType("System.String")),
                    new DataColumn("F_RequestTime", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM出库申请主表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOutbundPlanDataTableScheme()
        {
            return new DataTable("T_ScmOutboundPlanMain")
            {
                Columns =
                {
                    new DataColumn("F_RemotePlanCode", Type.GetType("System.String")),
                    new DataColumn("F_PlanType", Type.GetType("System.String")),
                    new DataColumn("F_ModifyQtyFlag", Type.GetType("System.String")),
                    new DataColumn("F_CustomerCode", Type.GetType("System.String")),
                    new DataColumn("F_RequestTime", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM入库申请细表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInboundPlanListDataTableScheme()
        {
            return new DataTable("T_ScmInboundPlanList")
            {
                Columns =
                {
                    new DataColumn("F_LineId", Type.GetType("System.String")),
                    new DataColumn("F_DeliveryNo", Type.GetType("System.String")),
                    new DataColumn("F_PurchaseNo", Type.GetType("System.String")),
                    new DataColumn("F_PurchaseLineId", Type.GetType("System.String")),
                    new DataColumn("F_ScmWarehouseCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_ConsignSaleFlag", Type.GetType("System.String")),
                    new DataColumn("F_FactoryCode", Type.GetType("System.String")),
                    new DataColumn("F_SupplierCode", Type.GetType("System.String")),
                    new DataColumn("F_ManuDate", Type.GetType("System.String")),
                    new DataColumn("F_MaterialUnit", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal")),
                    new DataColumn("F_StorageTime", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM出库申请细表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOutboundPlanListDataTableScheme()
        {
            return new DataTable("T_ScmOutboundPlanList")
            {
                Columns =
                {
                    new DataColumn("F_LineId", Type.GetType("System.String")),
                    new DataColumn("F_ScmWarehouseCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_FactoryCode", Type.GetType("System.String")),
                    new DataColumn("F_SupplierCode", Type.GetType("System.String")),
                    new DataColumn("F_Qty", Type.GetType("System.Decimal")),
                    new DataColumn("F_Remark", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// 物料状态变更申请表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMaterialStatusChangeDataTableScheme()
        {
            return new DataTable("T_MaterialStatusChange")
            {
                Columns =
                {
                    new DataColumn("F_ScmReqNo", System.Type.GetType("System.String")),
                    new DataColumn("F_ScmReqLineId", System.Type.GetType("System.String")),
                    new DataColumn("F_QualityStatus", System.Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM物料基础信息同步表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSyncMaterialDataTableScheme()
        {
            return new DataTable("T_SyncMaterialList")
            {
                Columns =
                {
                    new DataColumn("F_MaterialCode", Type.GetType("System.String")),
                    new DataColumn("F_MaterialName", Type.GetType("System.String")),
                    new DataColumn("F_FactoryCode", Type.GetType("System.String")),
                    new DataColumn("F_Unit", Type.GetType("System.String")),
                    new DataColumn("F_Weight", Type.GetType("System.String")),
                    new DataColumn("F_Keeper", Type.GetType("System.String")),
                    new DataColumn("F_PurchaseGroup", Type.GetType("System.String")),
                    new DataColumn("F_PurchaseGroupName", Type.GetType("System.String")),
                    new DataColumn("F_UseDate", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String")),
                    new DataColumn("F_Temp1", Type.GetType("System.String")),
                    new DataColumn("F_Temp2", Type.GetType("System.String")),
                    new DataColumn("F_Temp3", Type.GetType("System.String")),
                    new DataColumn("F_Temp4", Type.GetType("System.String")),
                    new DataColumn("F_Temp5", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM供应商基础信息同步表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSyncSupplierDataTableScheme()
        {
            return new DataTable("T_SyncSupplierList")
            {
                Columns =
                {
                    new DataColumn("F_SupplierNo", Type.GetType("System.String")),
                    new DataColumn("F_SupplierName", Type.GetType("System.String")),
                    new DataColumn("F_FactoryCode", Type.GetType("System.String")),
                    new DataColumn("F_Address", Type.GetType("System.String")),
                    new DataColumn("F_Telphone", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String")),
                    new DataColumn("F_Temp1", Type.GetType("System.String")),
                    new DataColumn("F_Temp2", Type.GetType("System.String"))
                }
            };
        }
        /// <summary>
        /// SCM客户基础信息同步表结构
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSyncCustomerDataTableScheme()
        {
            return new DataTable("T_SyncCustomerList")
            {
                Columns =
                {
                    new DataColumn("F_CustomerNo", Type.GetType("System.String")),
                    new DataColumn("F_CustomerName", Type.GetType("System.String")),
                    new DataColumn("F_FactoryCode", Type.GetType("System.String")),
                    new DataColumn("F_Address", Type.GetType("System.String")),
                    new DataColumn("F_Telphone", Type.GetType("System.String")),
                    new DataColumn("F_Remark", Type.GetType("System.String")),
                    new DataColumn("F_Temp1", Type.GetType("System.String")),
                    new DataColumn("F_Temp2", Type.GetType("System.String"))
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
                    new DataColumn("F_ReportType", System.Type.GetType("System.String")),
                    new DataColumn("F_WmsTaskID", System.Type.GetType("System.String")),
                    new DataColumn("F_WcsTaskID", System.Type.GetType("System.String")),
                    new DataColumn("F_ErrorMsg", System.Type.GetType("System.String")),
                    new DataColumn("F_StationID", System.Type.GetType("System.String")),
                    new DataColumn("F_ReportVal", System.Type.GetType("System.String")),
                    new DataColumn("F_ReportTime", System.Type.GetType("System.String"))
                }
            };
        }
        #endregion
    }
}