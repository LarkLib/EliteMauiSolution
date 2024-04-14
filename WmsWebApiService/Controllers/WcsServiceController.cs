using EliteHelper;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// WCS相关服务接口
    /// </summary>
    [RoutePrefix("api/WcsService")]
    public class WcsServiceController : ApiController
    {
        /// <summary>
        /// WCS任务信息上报服务接口
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WcsReport")]
        public WcsResponseBody WcsReport([FromBody] WcsReportDataBody body)
        {
            WcsResponseBody response = new WcsResponseBody();

            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                }
                else
                {
                    if (!CommonFunc.ValidateReportType(body.F_ReportType, "normal"))
                        throw new Exception($"未知的上报类型码：{body.F_ReportType}");

                    DataTable tableReport = CommonFunc.GetWcsReportDataTableScheme();
                    DataRow row = tableReport.NewRow();
                    row["F_ReportType"] = body.F_ReportType;
                    row["F_WmsTaskID"] = body.F_WmsTaskID;
                    row["F_WcsTaskID"] = body.F_WcsTaskID;
                    row["F_ErrorMsg"] = body.F_ErrorMsg;
                    row["F_StationID"] = body.F_StationID;
                    row["F_ReportVal"] = body.F_ReportVal;
                    row["F_ReportTime"] = body.F_ReportTime;
                    tableReport.Rows.Add(row);

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@report", SqlDbType.Structured),
                        new SqlParameter("@message",SqlDbType.NVarChar, 500),
                        new SqlParameter("@return",SqlDbType.NVarChar, 50)
                    };

                    parameters[0].Value = tableReport;
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2].Direction = ParameterDirection.ReturnValue;

                    SqlDbHelper.ExecuteNonQuery("Elite_P_Project_WcsReport", parameters);

                    response.ReqNo = body.F_TimeStamp;
                    response.Code = parameters[2].Value.ToString();
                    response.Msg = parameters[2].Value.ToString().Equals("200") ? "succeed" : parameters[1].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                response.ReqNo = body.F_TimeStamp;
                response.Code = "300";
                response.Msg = ex.Message;
            }

            return response;
        }
    }
}
