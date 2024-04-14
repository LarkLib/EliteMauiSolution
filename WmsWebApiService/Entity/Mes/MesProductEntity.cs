using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 成品入库请求实体类
    /// </summary>
    public class ProductInboundRequestBody
    {
        /// <summary>
        /// 申请任务号
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// 机台号
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// 包装类型
        /// </summary>
        public string MaterialPS { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }

        /// <summary>
        /// 入库成品信息
        /// </summary>
        public List<ProductBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 入库成品信息
    /// </summary>
    public class ProductBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 箱条码
        /// </summary>
        public string MaterialBarcode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Qty { get; set; }

    }
}