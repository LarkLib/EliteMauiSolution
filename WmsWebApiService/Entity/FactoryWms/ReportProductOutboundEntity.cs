using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 成品出库上报实体类
    /// </summary>
    public class ReportProductOutboundBody
    {
        /// <summary>
        /// 上报时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 上报信息
        /// </summary>
        public List<ProductOutboundBody> MaterialList { get; set; }
    }
    /// <summary>
    /// 出库成品信息
    /// </summary>
    public class ProductOutboundBody
    {
        /// <summary>
        /// 物料条码
        /// </summary>
        public string MaterialBarcode { get; set; } = "";
        /// <summary>
        /// 出库申请：申请数量；返库申请：WMS送料时传入的数量
        /// </summary>
        public decimal Qty { get; set; } = 0;
    }
}