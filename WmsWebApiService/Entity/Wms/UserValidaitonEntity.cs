using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 用户校验申请信息
    /// </summary>
    public class UserValidaitonRequestBody
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPwd { get; set; }
    }

    /// <summary>
    /// 用户校验返回信息
    /// </summary>
    public class UserValidationResponseBody
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
        /// 用户信息
        /// </summary>
        public UserBody Data { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserBody
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 关联角色索引
        /// </summary>
        public long RelationRoleID { get; set; }
        /// <summary>
        /// 关联角色名称
        /// </summary>
        public string RelationRoleName { get; set; }
    }
}