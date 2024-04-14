using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 计划明细查询申请实体类
    /// </summary>
    public class PlanListQueryRequestBody
    {
        /// <summary>
        /// wms本地计划编号
        /// </summary>
        public List<string> LocalPlanCodes { get; set; }
    }

    /// <summary>
    /// 计划明细查询返回实体类
    /// </summary>
    public class PlanListQueryResponseBody
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
        /// 计划主表信息
        /// </summary>
        public List<PlanListBody> Data { get; set; } = null;
    }

    /// <summary>
    /// 计划明细信息实体类
    /// </summary>
    public class PlanListBody
    { 
        /// <summary>
        /// 计划明细索引
        /// </summary>
        public long PlanListID { get; set; }
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
        /// 物料类别索引
        /// </summary>
        public long MaterialCategoryID { get; set; }
        /// <summary>
        /// 物料类别名称
        /// </summary>
        public string MaterialCategoryName { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 消耗数量
        /// </summary>
        public decimal ConsumedQty { get; set; }
        /// <summary>
        /// 空轴标识
        /// </summary>
        public string EmptyFlag { get; set; }
        /// <summary>
        /// 包装类型索引
        /// </summary>
        public long PackingCategory { get; set; }
        /// <summary>
        /// 包装类型名称
        /// </summary>
        public string PackingCategoryName { get; set; }
        /// <summary>
        /// 计划明细状态编码
        /// </summary>
        public string PlanListStatus { get; set; }
        /// <summary>
        /// 计划明细状态名称
        /// </summary>
        public string PlanListStatusName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public string CompleteTime { get; set; }
    }
}