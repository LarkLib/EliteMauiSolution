using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Policy;
using Wms.Web.Api.Service.Filters;
using Wms.Web.Api.Service.Model;

namespace WmsWebApiServiceCore.Controllers
{
    [Route("AGV")]
    [ApiController]
    public class AGVTaskController : ControllerBase
    {
        [HttpPost("CallPalletTask")]
        public AGVTaskResult CallPalletTask([FromBody] AGVTaskInfo content)
        {
            var reveiveContent = content;
            return new AGVTaskResult() { Result = 1, ErrCode = "任务下发成功!" };
        }

        /// <summary>
        /// 获取仓库站台信息
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStationByWarehouse")]
        public StationQueryResponseBody GetStationByWarehouse([FromBody] StationQueryRequestBody body)
        {
            StationQueryResponseBody response = new StationQueryResponseBody();
            try
            {
                if (body == null)
                {
                    response.Code = "100";
                    response.Msg = "Json序列化失败";
                    response.Data = null;
                }
                else
                {
                    int stationType = body.StationType.ToLower().Equals("in") ? 3 : 4;
                    DataTable table = null;//SqlDbHelper.GetDataSet($"select F_ID,F_CellCode from T_Base_StoreCell where F_WarehouseID={body.WarehouseID} and F_StationType={stationType} and F_CellType='Station' order by F_ID desc").Tables[0] ??
                        throw new Exception("获取站台信息失败，请检查网络连接或与开发人员联系.");

                    List<StationBody> stations = new List<StationBody>();
                    foreach (DataRow row in table.Rows)
                    {
                        StationBody station = new StationBody
                        {
                            StationID = Convert.ToInt64(row["F_ID"]),
                            StationName = row["F_CellCode"].ToString()
                        };

                        stations.Add(station);
                    }

                    response.Data = stations;
                }
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Msg = ex.Message;
                response.Data = null;
            }
            return response;
        }
    }
    public class StationQueryResponseBody
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; } = "200";
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Msg { get; set; } = "succeed";
        /// <summary>
        /// 站台信息
        /// </summary>
        public List<StationBody> Data { get; set; }
    }
    public class StationQueryRequestBody
    {
        /// <summary>
        /// 仓库索引
        /// </summary>
        public long WarehouseID { get; set; } = -1;
        /// <summary>
        /// 站台类型；In:入库站台    Out:出库站台
        /// </summary>
        public string StationType { get; set; } = "In";
    }
    /// <summary>
    /// 站台信息实体类
    /// </summary>
    public class StationBody
    {
        /// <summary>
        /// 站台索引
        /// </summary>
        public long StationID { get; set; }
        /// <summary>
        /// 站台名称
        /// </summary>
        public string StationName { get; set; }
    }


    public class AGVTaskInfo
    {
        public string TaskNo { get; set; }
        public string StartLoc { get; set; }
        public int StartCode { get; set; }
        public int StartHeight { get; set; }
        public string EndLoc { get; set; }
        public int EndCode { get; set; }
        public int EndHeight { get; set; }
        public int Priority { get; set; }
        public int TaskType { get; set; }
        public int Pallettype { get; set; }
        public string Remark { get; set; }
        public string Tmestamp { get; set; }
    }


    public class AGVTaskResult
    {
        public int Result { get; set; }
        public string ErrCode { get; set; }
    }

}
