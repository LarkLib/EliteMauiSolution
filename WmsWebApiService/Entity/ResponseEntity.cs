using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// wms服务接口返回信息
    /// </summary>
    public class WmsResponseBody
    {
        /// <summary>
        /// 请求任务号
        /// </summary>
        public string TaskCode { get; set; } = "";
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "succeed";
    }

    /// <summary>
    /// wcs服务接口返回信息
    /// </summary>
    public class WcsResponseBody
    {
        /// <summary>
        /// 请求任务号
        /// </summary>
        public string ReqNo { get; set; } = "";
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "succeed";
    }

    /// <summary>
    /// 厂级WMS服务接口返回信息
    /// </summary>
    public class FactoryWmsResponseBody
    {
        /// <summary>
        /// 成功标志; false 失败，true 成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 报错信息;返回标志为 0 时必填
        /// </summary>
        public string Message { get; set; } = "";
    }
    /// <summary>
    /// AGV服务接口返回信息
    /// </summary>
    public class AgvResponseBody
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "succeed";
    }
}