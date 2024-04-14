//using EliteHelper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// 任务信息上报服务
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WcsNormalReportController : ControllerBase
    {
        private readonly ISqlSugarClient db;

        public WcsNormalReportController(ISqlSugarClient db)
        {
            this.db = db;
        }

        /// <summary>
        /// WCS任务信息上报服务接口
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBody Post([FromBody] WcsReportDataBody body)
        {
            ResponseBody re = new ResponseBody();

            try
            {
                if (body == null)
                {
                    re.ReqNo = "";
                    re.Code = "100";
                    re.Msg = "Json序列化失败";
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

                    SugarParameter[] parameters =
                    {
                        new SugarParameter("@report", tableReport,"table_wcsReport"),
                        new SugarParameter("@message",null,true),
                        new SugarParameter("@return",null,System.Data.DbType.String,ParameterDirection.ReturnValue)
                    };

                    //parameters[0].Value = tableReport;
                    //parameters[1].Direction = ParameterDirection.Output;
                    //parameters[2].Direction = ParameterDirection.ReturnValue;

                    db.Ado.UseStoredProcedure().ExecuteCommand("Elite_P_Project_WcsReport", parameters);

                    re.ReqNo = body.F_TimeStamp;
                    re.Code = parameters[2].Value.ToString();
                    re.Msg = parameters[2].Value.ToString().Equals("200") ? "succeed" : parameters[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                re.ReqNo = body.F_TimeStamp;
                re.Code = "300";
                re.Msg = ex.Message;
            }

            return re;
        }
    }
}
