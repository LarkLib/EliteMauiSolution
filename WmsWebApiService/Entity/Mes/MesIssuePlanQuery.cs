using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 领料计划查询
    /// </summary>
    public class MesIssuePlanQuery
    {
        /// <summary>
        /// 申请计划号
        /// </summary>
        public string PlanCode { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "succeed";
        /// <summary>
        /// 计划明细
        /// </summary>
        public List<IssuePlanList> PlanList { get; set; } = null;
    }

    /// <summary>
    /// 计划明细信息
    /// </summary>
    public class IssuePlanList
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 发料数量
        /// </summary>
        public decimal IssueQty { get; set; }
    }
}