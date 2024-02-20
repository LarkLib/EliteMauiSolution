namespace Wms.Web.Api.Service
{
    /// <summary>
    /// wcs上报返回信息
    /// </summary>
    public class ResponseBody
    {
        /// <summary>
        /// 请求任务号
        /// </summary>
        public string ReqNo { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
    }

    /// <summary>
    /// Pda业务申请返回信息
    /// </summary>
    public class ServiceResponceBody
    {
        /// <summary>
        /// 状态码； 200：成功     500：失败
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 失败返回的异常信息
        /// </summary>
        public string Msg { get; set; }
    }
}