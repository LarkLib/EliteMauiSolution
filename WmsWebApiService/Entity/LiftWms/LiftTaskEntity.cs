using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 垂直升降库物料请求实体类
    /// </summary>
    public class LiftMaterialOutboundRequestBody
    {
        /// <summary>
        /// Mes申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 同步时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 用户名；格式：用户编号+用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 物料信息
        /// </summary>
        public List<LiftMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 垂直升降库出库分配物料实体类
    /// </summary>
    public class LiftMaterialOutboundResponseBody
    {
        /// <summary>
        /// Mes申请任务号
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 物料信息
        /// </summary>
        public List<LiftMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 垂直升降库物料信息实体类
    /// </summary>
    public class LiftMaterialBody
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
        /// 物料条码
        /// </summary>
        public string MaterialBarcode { get; set; } = "";
        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; } = "";
    }
}