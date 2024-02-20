using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.WmsModules.Services
{
    internal class WmsService
    {
        public static BaseMaterialResponseBody GetMaterialByCodeAsync(string materialCode)
        {
            var httpClient = new HttpClient();
            var client = new SwaggerClient("http://192.168.2.35:8030", httpClient);
            var result = client.GetMaterialByCodeAsync(materialCode).GetAwaiter().GetResult();
            return result;
            //var ret = ServiceProxy.GetMaterialByCodeAsync(materialCode);
            //return ret;
            //HttpClient client = new HttpClient();
            //var response = client.GetStringAsync("https://cn.bing.com/").GetAwaiter().GetResult();
            //var response = client.GetStringAsync("http://192.168.2.35:8030/mauiApi/getMaterialByCode?materialCode=CF-000151").GetAwaiter().GetResult();
        }
    }
}
