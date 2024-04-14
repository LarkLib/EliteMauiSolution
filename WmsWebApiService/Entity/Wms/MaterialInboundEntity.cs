namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 原材料入库申请实体类
    /// </summary>
    public class MaterialInboundRequestBody
    {
        /// <summary>
        /// 仓库索引
        /// </summary>
        public long WarehouseID { get; set; }
        /// <summary>
        /// 站台索引
        /// </summary>
        public long StationID { get; set; }
        /// <summary>
        /// 物料条码
        /// </summary>
        public string MaterialBarcode { get; set; }
        /// <summary>
        /// 操作员索引
        /// </summary>
        public long OperatorID { get; set; }
    }
}