using Wms.Web.Api.Service.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Wms.Web.Api.Service.Filters
{
    public class ApiLogFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public ApiLogFilter(ILogger<ApiLogFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionArguments = context.ActionArguments.ToJson();
            var resultContext = await next();
            string url = $"{resultContext.HttpContext.Request.Host}{resultContext.HttpContext.Request.Path}{resultContext.HttpContext.Request.QueryString.Value}";
            string method = resultContext.HttpContext.Request.Method;
            dynamic result = resultContext.Result == null || resultContext.Result.GetType().Name == "EmptyResult" ? new { Value = "EmptyResult" } : resultContext.Result as dynamic;
            string response = JsonConvert.SerializeObject(result.Value);
            _logger.LogInformation($"URL: {url} \n" +
                                   $"Method: {method} \n" +
                                   $"ActionArguments: {actionArguments} \n" +
                                   $"Response: {response} \n");
        }
    }
}
