using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 原料组箱申请实体类
    /// </summary>
    public class MaterialGroupBoxRequestBody
    {
        /// <summary>
        /// wms本地计划编号
        /// </summary>
        public string LocalPlanCode { get; set; }
        /// <summary>
        /// 操作员索引
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 物料组箱条码信息
        /// </summary>
        public List<MaterialBarcodesBody> Barcodes { get; set; }
    }

    /// <summary>
    /// 组箱条码信息实体类
    /// </summary>
    public class MaterialBarcodesBody
    {
        /// <summary>
        /// 物料条码
        /// </summary>
        public string Barcode { get; set; }
    }
}