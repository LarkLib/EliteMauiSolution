﻿using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Wms.Web.Api.Service.Extensions
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            if (context == null) return "";
            var result = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(result))
            {
                result = context.Connection.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(result) || result.Contains("::1"))
                result = "127.0.0.1";

            result = result.Replace("::ffff:", "127.0.0.1");
            result = IsIP(result) ? result : "127.0.0.1";
            return result;
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取登录用户id
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static long GetUId(this HttpContext context)
        {
            var uid = context.User.FindFirstValue(ClaimTypes.PrimarySid);

            return !string.IsNullOrEmpty(uid) ? long.Parse(uid) : 0;
        }

        /// <summary>
        /// 获取登录用户名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string? GetName(this HttpContext context)
        {
            var uid = context.User?.Identity?.Name;

            return uid;
        }

        /// <summary>
        /// 获取请求令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetToken(this HttpContext context)
        {
            return context.Request.Headers["Authorization"];
        }

        public static string? GetRequestUrl(this HttpContext context)
        {
            return context != null ? context.Request.Path.Value : "";
        }
        public static string GetQueryString(this HttpContext context)
        {
            return context != null ? context.Request.QueryString.Value : "";
        }

        /// <summary>
        /// 设置请求参数
        /// </summary>
        /// <param name="operLog"></param>
        /// <param name="context"></param>
        //public static void GetRequestValue(this HttpContext context, SysOperLog operLog)
        //{
        //    string reqMethod = operLog.RequestMethod;
        //    string param;

        //    if (HttpMethods.IsPost(reqMethod) || HttpMethods.IsPut(reqMethod) || HttpMethods.IsDelete(reqMethod))
        //    {
        //        context.Request.Body.Seek(0, SeekOrigin.Begin);
        //        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
        //        //需要使用异步方式才能获取
        //        param = reader.ReadToEndAsync().Result;
        //        if (param.IsEmpty())
        //        {
        //            param = GetQueryString(context);
        //        }
        //    }
        //    else
        //    {
        //        param = GetQueryString(context);
        //    }
        //    operLog.OperParam = param;
        //}
    }
}
