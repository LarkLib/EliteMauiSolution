using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 成品备货请求实体类
    /// </summary>
    public class ProductPrepareRequestBody
    {
        /// <summary>
        /// 申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 备货成品信息
        /// </summary>
        public List<ProductPrepareBody> MaterialList { get; set; }
    }
    /// <summary>
    /// 备货成品信息
    /// </summary>
    public class ProductPrepareBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Qty { get; set; }
    }
}