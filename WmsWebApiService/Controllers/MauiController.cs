using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Data;
using Wms.Web.Api.Service;
using WmsWebApiService.Entity;

namespace WmsWebApiService.Controllers
{
    //[Route("api/[controller]")]
    [Route("mauiApi")]
    [ApiController]
    public class MauiController : ControllerBase
    {
        private readonly ISqlSugarClient db;

        public MauiController(ISqlSugarClient db)
        {
            this.db = db;
        }

        [HttpGet("getMaterialByCode")]
        public BaseMaterialResponseBody GetMaterialByCode(string materialCode)
        {

            var re = new BaseMaterialResponseBody();
            try
            {
                if (materialCode == null)
                {
                    re.Code = "100";
                    re.Msg = "参数不能为空";
                }
                else
                {
                    re.Data = db.Queryable<BaseMaterial>().Where(m => m.F_MaterialCode == materialCode).ToList();
                    re.Count = re.Data.Count();
                    re.Code = "200";
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
