using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 仓库信息
    /// </summary>
    public class WarehouseBody
    {
        /// <summary>
        /// 仓库索引
        /// </summary>
        public long WarehouseID { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WarehouseCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; }
    }

    /// <summary>
    /// 仓库查询返回的信息
    /// </summary>
    public class WarehouseQueryResponseBody
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
        /// 返回的仓库信息
        /// </summary>
        public List<WarehouseBody> Data { get; set; }
    }
}