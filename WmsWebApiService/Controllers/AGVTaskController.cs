using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
