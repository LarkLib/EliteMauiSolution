using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 计划查询返回实体类
    /// </summary>
    public class PlanQueryResponseBody
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
        public List<PlanBody> Data { get; set; } = null;
    }

    /// <summary>
    /// 计划主表信息实体类
    /// </summary>
    public class PlanBody
    {
        /// <summary>
        /// 计划索引
        /// </summary>
        public long PlanID { get; set; }
        /// <summary>
        /// 自动执行标识；默认为0，手工执行
        /// </summary>
        public int AutoFlag { get; set; } = 0;
        /// <summary>
        /// wms本地计划编号
        /// </summary>
        public string LocalPlanCode { get; set; }
        /// <summary>
        /// Mes申请任务号
        /// </summary>
        public string RemotePlanCode { get; set; }
        /// <summary>
        /// 计划类型编码
        /// </summary>
        public string PlanType { get; set; }
        /// <summary>
        /// 计划类型名称
        /// </summary>
        public string PlanTypeName { get; set; }
        /// <summary>
        /// 计划状态编码
        /// </summary>
        public string PlanStatus { get; set; }
        /// <summary>
        /// 计划状态名称
        /// </summary>
        public string PlanStatusName { get; set; }
        /// <summary>
        /// Mes要料机台索引
        /// </summary>
        public long StationID { get; set; }
        /// <summary>
        /// Mes要料机台名称
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 计划创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string FromWarehouse { get; set; }
    }

    /// <summary>
    /// 申请计划执行实体类
    /// </summary>
    public class PlanExecuteRequestBody
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
        /// 操作员编码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string UserName { get; set; }
    }
}