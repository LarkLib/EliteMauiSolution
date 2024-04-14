namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 获取厂级WMS接口访问TOKEN入参实体
    /// </summary>
    public class FactoryWmsTokenInParamsBody
    {
        /// <summary>
        /// 功能ID;默认值rest_app
        /// </summary>
        public string Funid { get; set; } = "rest_app";
        /// <summary>
        /// 方法名;默认值get_token
        /// </summary>
        public string Eventcode { get; set; } = "get_token";
        /// <summary>
        /// 用户名;cclk
        /// </summary>
        public string User_code { get; set; } = "";
        /// <summary>
        /// 编码;cclk
        /// </summary>
        public string appid { get; set; } = "";
        /// <summary>
        /// 密码;cclk123@cclk
        /// </summary>
        public string Appsecret { get; set; } = "";
    }

    /// <summary>
    /// 获取厂级WMS接口访问TOKEN出参实体
    /// </summary>
    public class FactoryWmsTokenOutParamsBody
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
        /// 返回的TOKEN信息
        /// </summary>
        public FactoryWmsTokenBody Data { get; set; }
    }

    /// <summary>
    /// 厂级WMS返回的TOKEN
    /// </summary>
    public class FactoryWmsTokenBody
    {
        /// <summary>
        /// token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 有效期 7200 秒
        /// </summary>
        public int Expires_in { get; set; }
    }
}