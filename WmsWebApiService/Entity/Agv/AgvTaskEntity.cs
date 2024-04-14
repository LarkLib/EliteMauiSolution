namespace Wms.Web.Api.Service
{
    /// <summary>
    /// AGV任务信息实体类
    /// </summary>
    public class AgvTaskBody
    {
        /// <summary>
        /// 索引；默认与TaskIndex相同
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        public long TaskIndex { get; set; }
        /// <summary>
        /// 任务类型；默认104
        /// </summary>
        public int Type { get; set; } = 8;
        /// <summary>
        /// 任务优先级；默认0
        /// </summary>
        public int Priority { get; set; } = 0;
        /// <summary>
        /// 起点坐标
        /// </summary>
        public string StartPoint { get; set; }
        /// <summary>
        /// 终点坐标
        /// </summary>
        public string EndPoint { get; set; }
        /// <summary>
        /// 状态值；默认0
        /// </summary>
        public int State { get; set; } = 0;
    }

    /// <summary>
    /// AGV上报信息实体类
    /// </summary>
    public class AgvReportBody
    {
        /// <summary>
        /// 任务号
        /// </summary>
        public long TaskIndex { get; set; }
        /// <summary>
        /// 状态值；默认0
        /// </summary>
        public int State { get; set; } = 0;
    }
}