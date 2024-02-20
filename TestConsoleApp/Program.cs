using MauiWmsServiceProxy;

namespace TestConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var ret = GetMaterialByCodeAsync("CF-000151").Result;
    }

    public static async Task<BaseMaterialResponseBody> GetMaterialByCodeAsync(string materialCode)
    {
        //var httpClient = new HttpClient();
        //var client = new swaggerClient("http://192.168.2.35:8030", httpClient);
        //var result = await client.GetMaterialByCodeAsync(materialCode);
        //return result;
        var ret = ServiceProxy.GetMaterialByCodeAsync(materialCode);
        return ret;
    }
}
