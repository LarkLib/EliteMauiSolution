using System.Collections.Generic;

namespace Wms.Web.Api.Service.Entity
{
    /// <summary>
    /// 成品上架申请实体类
    /// </summary>
    public class ProductStoreToShelvesRequestBody
    {
        /// <summary>
        /// 站台索引
        /// </summary>
        public long StationID { get; set; }
        /// <summary>
        /// 成品箱码信息
        /// </summary>
        public List<BoxBarcodesBody> BoxBarcodes { get; set; }
    }
}