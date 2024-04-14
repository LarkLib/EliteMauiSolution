namespace Wms.Web.Api.Service
{
    /// <summary>
    /// wcs上报信息
    /// </summary>
    public class WcsReportDataBody
    {
        /// <summary>
        /// 上报任务号
        /// </summary>
        public string F_TimeStamp { get; set; }
        /// <summary>
        /// 上报类型：
        /// PALLET_SIZE_ERROR：托盘超宽
        /// BARCODE：托盘条码
        /// PALLET_HEIGHT：托盘高度
        /// PALLET_WEIGHT：托盘重量
        /// VISION_CHECK：视觉核验
        /// TASK_REPORT：任务状态
        /// SWITCH_GROUP:开关组合值
        /// </summary>
        public string F_ReportType { get; set; }
        /// <summary>
        /// 管理任务索引
        /// </summary>
        public string F_WmsTaskID { get; set; }
        /// <summary>
        /// 调度任务索引
        /// </summary>
        public string F_WcsTaskID { get; set; }
        /// <summary>
        /// 异常消息
        /// </summary>
        public string F_ErrorMsg { get; set; }
        /// <summary>
        /// 站台索引
        /// </summary>
        public string F_StationID { get; set; }
        /// <summary>
        /// 上报内容
        /// </summary>
        public string F_ReportVal { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public string F_ReportTime { get; set; }
    }
}