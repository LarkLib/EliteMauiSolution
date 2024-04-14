using EliteHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// Agv相关服务接口
    /// </summary>
    [RoutePrefix("api/AgvService")]
    public class AgvServiceController : ApiController
    {
        private readonly AppConfig _appConfig = new AppConfig();
        /// <summary>
        /// 发送AGV任务服务接口
        /// </summary>
        /// <param name="agvTasks"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendTaskToAgv")]
        public WmsResponseBody SendTaskToAgv([FromBody] List<AgvTaskBody> agvTasks)
        {
            WmsResponseBody response = new WmsResponseBody();

            try
            {
                string json = JsonHelper.ObjectToJson(agvTasks);

                //记录发送任务信息日志
                LoggerHelper.LogAgv($"下达Agv任务_发送信息；{json}.");

                var result = CustomHttpClient.PostData(_appConfig.GetKeyValue("AgvUrl"), json);

                //记录返回的信息
                LoggerHelper.LogAgv($"下达Agv任务_返回信息：{result}");

                if (string.IsNullOrWhiteSpace(result))
                    throw new Exception("下达Agv任务返回信息为空.");

                var agvResponse = JsonHelper.JsonToObject<AgvResponseBody>(result);

                if (!agvResponse.Code.Equals("200"))
                    throw new Exception(agvResponse.Msg);
            }
            catch (Exception ex)
            {
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// AGV上报任务状态服务接口
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgvReport")]
        public AgvResponseBody AgvReport([FromBody] AgvReportBody body)
        {
            AgvResponseBody response = new AgvResponseBody();

            try
            {
                if (body != null)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@F_TaskID", SqlDbType.BigInt),
                        new SqlParameter("@F_TaskStatus", SqlDbType.NVarChar,255),
                        new SqlParameter("@message",SqlDbType.NVarChar, 500),
                        new SqlParameter("@return",SqlDbType.NVarChar, 50)
                    };

                    parameters[0].Value = body.TaskIndex;
                    parameters[1].Value = body.State;
                    parameters[2].Direction = ParameterDirection.Output;
                    parameters[3].Direction = ParameterDirection.ReturnValue;

                    SqlDbHelper.ExecuteNonQuery("Elite_P_Project_AgvReport", parameters);

                    response.Code = parameters[3].Value.ToString();
                    response.Msg = parameters[3].Value.ToString().Equals("200") ? "succeed" : parameters[2].Value.ToString();
                }
                else
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
            }
            catch (Exception ex)
            {
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
    }
}
