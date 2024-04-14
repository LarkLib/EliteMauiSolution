using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// WMS管理物料基础信息查询实体
    /// </summary>
    public class MaterialQueryResponseBody
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";
        /// <summary>
        /// 物料信息
        /// </summary>
        public List<LocalMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// WMS管理物料基础信息实体
    /// </summary>
    public class LocalMaterialBody
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 原材料规格
        /// </summary>
        public string MaterialSpec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string MaterialUnit { get; set; }
        /// <summary>
        /// 物料类别索引
        /// </summary>
        public string MaterialCategoryID { get; set; }
        /// <summary>
        /// 物料类别名称
        /// </summary>
        public string MaterialCategoryName { get; set; }
        /// <summary>
        /// 有效期(天)
        /// </summary>
        public string MaterialValidity { get; set; }
    }
}