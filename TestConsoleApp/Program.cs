namespace TestConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        GetData();
        //var ret = GetMaterialByCodeAsync("CF-000151").Result;
    }

    public static void GetData()
    {
        var httpClient = new HttpClient();
        var client = new PdaServiceClient(httpClient);

        client.BaseUrl = "http://192.168.2.35:8050";
        //var result = client.GetWarehouseAsync().GetAwaiter().GetResult();
        var result = client.GetStationByWarehouseAsync(new StationQueryRequestBody { WarehouseID = 1, StationType = "In" }).GetAwaiter().GetResult();
    }

    //public static async Task<BaseMaterialResponseBody> GetMaterialByCodeAsync(string materialCode)
    //{
    //    //var httpClient = new HttpClient();
    //    //var client = new swaggerClient("http://192.168.2.35:8030", httpClient);
    //    //var result = await client.GetMaterialByCodeAsync(materialCode);
    //    //return result;
    //    var ret = ServiceProxy.GetMaterialByCodeAsync(materialCode);
    //    return ret;
    //}
}
