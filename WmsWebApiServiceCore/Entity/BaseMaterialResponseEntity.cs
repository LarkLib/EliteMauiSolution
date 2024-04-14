using WmsWebApiService.Entity;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// </summary>
    public class BaseMaterialResponseBody
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        public List<BaseMaterial> Data { get; set; }
        public int Count {  get; set; }
    }
}