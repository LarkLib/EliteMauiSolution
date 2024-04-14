using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 垂直升降库库存校验实体
    /// </summary>
    public class LiftInventoryCheckBody
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
        /// 校验的物料信息
        /// </summary>
        public List<LiftInventoryCheckMaterialBody> MaterialList { get; set; }
    }

    /// <summary>
    /// 垂直升降库库存校验物料信息实体
    /// </summary>
    public class LiftInventoryCheckMaterialBody
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
        /// 库存数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 已分配数量
        /// </summary>
        public decimal AllocatedQty { get; set; } = 0;
    }
}