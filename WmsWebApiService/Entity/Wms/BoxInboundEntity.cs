namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 载具入库申请实体类
    /// </summary>
    public class BoxInboundRequestBody
    {
        /// <summary>
        /// 入库站台索引
        /// </summary>
        public long StationID { get; set; }
        /// <summary>
        /// 载具编号
        /// </summary>
        public string MaterialCode { get; set; }
    }
}