using Elite.LMS.Maui.Wms.Data;
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
        public static PdaServiceClient ServiceClient
        {
            get
            {
                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(5);
                var client = new PdaServiceClient(httpClient);
                client.BaseUrl = Config.ServiceBaseUrl;
                return client;
            }
        }
        public static WarehouseQueryResponseBody GetWarehouse()
        {
            return ServiceClient.GetWarehouseAsync().GetAwaiter().GetResult();
        }

        public static StationQueryResponseBody GetStationByWarehouse(long? warehouseId, string stationType)
        {
            return ServiceClient.GetStationByWarehouseAsync(new StationQueryRequestBody { WarehouseID = warehouseId, StationType = stationType }).GetAwaiter().GetResult();
        }

        public static WmsResponseBody MaterialInbound(MaterialInboundRequestBody body)
        {
            return ServiceClient.MaterialInboundAsync(body).GetAwaiter().GetResult();
        }

        public static WmsResponseBody MaterialReturnInbound(MaterialInboundRequestBody body)
        {
            return ServiceClient.MaterialReturnInboundAsync(body).GetAwaiter().GetResult();
        }
        public static UserValidationResponseBody LoginUserValidation(UserValidaitonRequestBody body)
        {
            return ServiceClient.LoginUserValidationAsync(body).GetAwaiter().GetResult();
        }
        public static PlanQueryResponseBody GetManuallyIssuePlan(int warehouse)
        {
            return ServiceClient.GetManuallyIssuePlanAsync(warehouse).GetAwaiter().GetResult();
        }
        public static WmsResponseBody ExecuteManuallyIssuePlan(PlanExecuteRequestBody body)
        {
            return ServiceClient.ExecuteManuallyIssuePlanAsync(body).GetAwaiter().GetResult();
        }
        public static WmsResponseBody ManuallyIssueMaterialCheck(string body)
        {
            return ServiceClient.ManuallyIssueMaterialCheckAsync(body).GetAwaiter().GetResult();
        }
    }
}
