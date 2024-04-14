namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 厂级WMS原材料入库上报信息实体
    /// </summary>
    public class FactoryWmsMaterialInboundReportBody
    {
        /// <summary>
        /// 功能ID;默认值 ware_cclkapi
        /// </summary>
        public string Funid { get; set; } = "ware_cclkapi";
        /// <summary>
        /// 方法名;默认值 matIn
        /// </summary>
        public string Eventcode { get; set; } = "matIn";
        /// <summary>
        /// 用户名
        /// </summary>
        public string User_code { get; set; }
        /// <summary>
        /// 获取到的 token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 上报原材料的标签号
        /// </summary>
        public string Labelno { get; set; }
    }

    /// <summary>
    /// 厂级WMS产成品入库上报信息实体
    /// </summary>
    public class FactoryWmsProductInboundReportBody
    {
        /// <summary>
        /// 功能ID;默认值 ware_cclkapi
        /// </summary>
        public string Funid { get; set; } = "ware_cclkapi";
        /// <summary>
        /// 方法名;默认值 prodIn
        /// </summary>
        public string Eventcode { get; set; } = "prodIn";
        /// <summary>
        /// 用户名
        /// </summary>
        public string User_code { get; set; }
        /// <summary>
        /// 获取到的 token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 上报原材料的标签号
        /// </summary>
        public string Labelno { get; set; }
        /// <summary>
        /// 入库库位标识
        /// </summary>
        public string Tlocation { get; set; }
    }
}