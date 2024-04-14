namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 人工退料消耗数查询实体类
    /// </summary>
    public class QueryMesMaterialRequestBody
    {
        public string ORIGID { get; set; } = "CCTH";
        public string USERID { get; set; } = "XinSong_WMS";
        public string USERNAME { get; set; } = "新松WMS";
        public string MODULE { get; set; } = "WMS";
        public string TYPE { get; set; } = "GetLabelUsedCount";
        public string PROJECT { get; set; } = "HMES-H9";
        public string PROJECTVER { get; set; } = "CCTH";
        public string TRACEDUMP { get; set; }
        public string LANG { get; set; } = "zh-CN";
        public string TOKEN { get; set; } = "1uyFJvxLmZsYCPWRZ4b+6KPW7ca4uwn6oDvb+/26aLY=";
        public string SIGNATURE { get; set; } = "";
        /// <summary>
        /// 数据体
        /// </summary>
        public QueryMaterialBody DATA { get; set; }
        /// <summary>
        /// Mes自定义数据
        /// </summary>
        public MesCustomData CUSTOMDATA { get; set; } = new MesCustomData();
    }

    /// <summary>
    /// Mes自定义数据实体类
    /// </summary>
    public class MesCustomData
    {
        public string program_id { get; set; } = "WMS";
        public string module_id { get; set; } = "BLL.CLIENT";
    }

    /// <summary>
    /// 要查询的物料信息
    /// </summary>
    public class QueryMaterialBody
    { 
        /// <summary>
        /// 物料条码
        /// </summary>
        public string QRCord { get; set; }
    }

    /// <summary>
    /// MES返回信息实体类
    /// </summary>
    public class MesResponseBody
    {
        /// <summary>
        /// 成功标识；0：失败   1：成功
        /// </summary>
        public string RESULT { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string STATUSCODE { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string MSG { get; set; }
        public string PROJECT { get; set; }
        public string PROJECTVER { get; set; }
        public string MODULE { get; set; }
        public string TYPE { get; set; }
        public ResultData DATA { get; set; }
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    public class ResultData
    { 
        /// <summary>
        /// 消耗数
        /// </summary>
        public decimal rel { get; set; }
    }
}