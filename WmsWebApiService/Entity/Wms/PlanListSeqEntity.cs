using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 计划指令查询返回信息实体类
    /// </summary>
    public class PlanSeqQueryResponseBody
    {
        /// <summary>
        /// 状态码 0：失败  大于0成功
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 失败返回的消息
        /// </summary>
        public string Msg { get; set; } = "succeed";
        /// <summary>
        /// 物料清单信息
        /// </summary>
        public List<PlanSeqQueryMaterialBody> Data { get; set; }
    }

    /// <summary>
    /// 计划指令物料清单实体类
    /// </summary>
    public class PlanSeqQueryMaterialBody
    {
        /// <summary>
        /// 存储仓库索引
        /// </summary>
        public long WarehouseID { get; set; }
        /// <summary>
        /// 存储仓库名称
        /// </summary>
        public string WarehouseName { get; set; }
        /// <summary>
        /// 存储位置
        /// </summary>
        public string StoreCellCode { get; set; }
        /// <summary>
        /// 物料索引
        /// </summary>
        public long MaterialID { get; set; }
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
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
    }
}