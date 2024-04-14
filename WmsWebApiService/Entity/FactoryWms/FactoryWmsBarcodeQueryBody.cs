using System.Collections.Generic;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 厂级WMS标签查询参数实体
    /// </summary>
    public class FactoryWmsBarcodeQueryInParamsBody
    {
        /// <summary>
        /// 功能ID;默认值 ware_cclkapi
        /// </summary>
        public string Funid { get; set; } = "ware_cclkapi";
        /// <summary>
        /// 方法名; 默认值 qryLabel
        /// </summary>
        public string Eventcode { get; set; } = "qryLabel";
        /// <summary>
        /// 用户名;cclk
        /// </summary>
        public string User_code { get; set; } = "";
        /// <summary>
        /// 获取到的 token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 要查询的标签号;支持多个，用逗号分隔
        /// </summary>
        public string Labelno { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class FactoryWmsBarcodeQueryOutParamsBody
    {
        /// <summary>
        /// 成功标志; false 失败，true 成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 报错信息;返回标志为 0 时必填
        /// </summary>
        public string Message { get; set; } = "";
        /// <summary>
        /// 返回的标签明细信息
        /// </summary>
        public List<FactoryWmsBarcodeDetailBody> Data { get; set; }
    }

    /// <summary>
    /// 标签信息实体
    /// </summary>
    public class FactoryWmsBarcodeDetailBody
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标签号
        /// </summary>
        public string Labelno { get; set;}
        /// <summary>
        /// 物料编号
        /// </summary>
        public string Itemcode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Itemname { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 库位标识
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 父项标签
        /// </summary>
        public string Flabelno { get; set; }
        /// <summary>
        /// 标签类型
        /// </summary>
        public string Labeltype { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batchno { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Activestatus { get; set; }
        /// <summary>
        /// 标签打印日期
        /// </summary>
        public string Labelprintdate { get; set; }
    }
}