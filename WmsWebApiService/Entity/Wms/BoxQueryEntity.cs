using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 载具查询返回的信息
    /// </summary>
    public class BoxQueryResponseBody
    {
        /// <summary>
        /// 状态码； 200：成功     500：失败
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 失败返回的异常信息
        /// </summary>
        public string Msg { get; set; } = "succeed";
        /// <summary>
        /// 返回的载具信息
        /// </summary>
        public List<BoxBody> Data { get; set; }
    }
    /// <summary>
    /// 载具信息
    /// </summary>
    public class BoxBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
    }
}