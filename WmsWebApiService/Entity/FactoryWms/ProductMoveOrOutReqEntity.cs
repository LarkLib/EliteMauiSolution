using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 成品上架或直接出库申请实体类
    /// </summary>
    public class ProductMoveOrOutReqBody
    {
        /// <summary>
        /// 上报时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 申请类型；Up:上架  Out:直接出库
        /// </summary>
        public string ReqType { get; set; }
        /// <summary>
        /// 条码信息
        /// </summary>
        public List<BoxBarcodesBody> BarcodeList { get; set; }
    }
    /// <summary>
    /// 成品箱条码信息
    /// </summary>
    public class BoxBarcodesBody
    {
        /// <summary>
        /// 成品箱条码
        /// </summary>
        public string Barcode { get; set; } = "";
    }
}