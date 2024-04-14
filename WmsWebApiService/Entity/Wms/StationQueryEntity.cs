using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 站台查询实体类
    /// </summary>
    public class StationQueryRequestBody
    {
        /// <summary>
        /// 仓库索引
        /// </summary>
        public long WarehouseID { get; set; } = -1;
        /// <summary>
        /// 站台类型；In:入库站台    Out:出库站台
        /// </summary>
        public string StationType { get; set; } = "In";
    }

    /// <summary>
    /// 站台查询返回信息实体类
    /// </summary>
    public class StationQueryResponseBody
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Msg { get; set; } = "succeed";
        /// <summary>
        /// 站台信息
        /// </summary>
        public List<StationBody> Data { get; set; }
    }

    /// <summary>
    /// 站台信息实体类
    /// </summary>
    public class StationBody
    { 
        /// <summary>
        /// 站台索引
        /// </summary>
        public long StationID { get; set;}
        /// <summary>
        /// 站台名称
        /// </summary>
        public string StationName { get; set; }
    }
}