using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// MES请求物料人工出库任务信息实体类
    /// </summary>
    public class MesRequestManuallyOutboundBody
    {
        /// <summary>
        /// 申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 申请机台号
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 任务明细信息
        /// </summary>
        public List<ManuallyOutboundMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 物料信息
    /// </summary>
    public class ManuallyOutboundMaterialBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 出库申请：申请数量；返库申请：WMS送料时传入的数量
        /// </summary>
        public decimal Qty { get; set; } = 0;
    }
}