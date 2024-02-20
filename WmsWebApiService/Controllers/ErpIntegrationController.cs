//using EliteHelper;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using WmsWebApiService.Entity;

namespace Wms.Web.Api.Service.Controllers
{
    /// <summary>
    /// 任务信息上报服务
    /// </summary>
    [ApiController]
    [Route("erpApi")]
    public class ErpIntegrationController : ControllerBase
    {
        private readonly ISqlSugarClient db;

        public ErpIntegrationController(ISqlSugarClient db)
        {
            this.db = db;
        }

        /// <summary>
        /// Erp任务接口
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("stockTask")]
        public ServiceResponceBody StockTask([FromBody] ErpStockTaskBody body)
        {
            var re = new ServiceResponceBody();
            var tasks = new[] { body };
            try
            {
                if (body == null)
                {
                    re.Code = "100";
                    re.Msg = "Json序列化失败";
                }
                else
                {

                    DataTable table = db.Ado.GetDataTable("declare @t table_erpTask;select * from @t;");
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        var task = tasks[i];
                        DataRow row = table.NewRow();
                        row["F_ID"] = i + 1;
                        row["F_PalletType"] = task.PalletType;
                        row["F_TaskType"] = task.TaskType;
                        row["F_PalletCode"] = task.PalletCode ?? task.PallectCode;
                        row["F_TaskCode"] = task.TaskCode;
                        row["F_MaterialCode"] = task.MaterialCode;
                        row["F_Qty"] = task.Quantity;
                        row["F_StationID"] = task.StationID;
                        table.Rows.Add(row);
                    }

                    SugarParameter[] parameters =
                    {
                        new SugarParameter("@task", table,"table_erpTask"),
                        new SugarParameter("@message",null,true),
                        new SugarParameter("@return",null,System.Data.DbType.String,ParameterDirection.ReturnValue)
                    };

                    db.Ado.UseStoredProcedure().ExecuteCommand("Elite_P_Project_CreateErpTask", parameters);

                    re.Code = parameters[2].Value.ToString();
                    re.Msg = parameters[2].Value.ToString().Equals("200") ? "succeed" : parameters[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                re.Code = "300";
                re.Msg = ex.Message;
            }

            return re;
        }
    }
}
