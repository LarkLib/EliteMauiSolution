using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// Mes发送机台旋转通知信息实体类
    /// </summary>
    public class MesStationRotationReportBody
    {
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ReqTime { get; set; }
        /// <summary>
        /// 机台信息
        /// </summary>
        public List<StationReportBody> StationList { get; set; }
    }

    /// <summary>
    /// 机台信息实体类
    /// </summary>
    public class StationReportBody
    { 
        /// <summary>
        /// 机台号
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// 机台关联的输送线编号
        /// </summary>
        public string LineCode { get; set; }
    }
}