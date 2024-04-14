using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Wms.Web.Api.Service
{
    /// <summary>
    /// 自定义通讯类
    /// </summary>
    public class CustomHttpClient
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="JosnData"></param>
        /// <returns></returns>
        public static string PostData(string Url, string JosnData)
        {
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(Url);
            wReq.Method = "Post";            
            wReq.ContentType = "application/json";

            //string js = HttpUtility.UrlEncode(JosnData);
            byte[] data = Encoding.UTF8.GetBytes(JosnData);

            wReq.ContentLength = data.Length;
            Stream reqStream = wReq.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();

            using (StreamReader sr = new StreamReader(wReq.GetResponse().GetResponseStream()))
            {
                string result = sr.ReadToEnd();

                return result;
            }
        }

        /// <summary>
        /// 发送键值对
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static string PostKeyValuePair(string Url, Dictionary<string, string> keyValues)
        {
            string result = "";
            //设置Http的正文
            var content = new FormUrlEncodedContent(keyValues);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                //异步Post
                HttpResponseMessage response = client.PostAsync(Url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }

            return result;
        }

        /// <summary>
        /// 发送信息到MES
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        public static string PostDataToMes(string Url, string JsonData)
        {
            WebClient wc = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            wc.Headers.Add("orgid", "CCTH");
            wc.Headers.Add("userid", "XinSong_WMS");
            wc.Headers.Add("token", "1uyFJvxLmZsYCPWRZ4b+6KPW7ca4uwn6oDvb+/26aLY=");
            wc.Headers.Add("timestamp", CommonFunc.GetTimeStamp13());
            wc.Headers.Add("nonce", "10");
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return wc.UploadString(Url, "post", $"={Convert.ToBase64String(Encoding.UTF8.GetBytes((HttpUtility.UrlEncode(JsonData))))}");
        }
    }
}