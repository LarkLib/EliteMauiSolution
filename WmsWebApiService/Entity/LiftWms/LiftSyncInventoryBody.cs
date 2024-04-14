using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 垂直升降库库存同步信息
    /// </summary>
    public class LiftSyncInventoryBody
    {
        /// <summary>
        /// 同步时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 任务类型：In(增加)，Out(减少)，Init(初始化)
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 库存明细列表
        /// </summary>
        public List<LiftInventoryItemBody> MaterialList { get; set; }
    }
    /// <summary>
    /// 垂直升降库库存明细信息
    /// </summary>
    public class LiftInventoryItemBody
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
        public string MaterialBarcode { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
    }
}