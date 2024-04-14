using Wms.Web.Api.Service.Extensions;
using Wms.Web.Api.Service.Model;

namespace Wms.Web.Api.Service
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            { 
                await _next(context);
            }
            catch (Exception e)
            {
                await ExceptionHandlerAsync(context, e);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            _logger.LogError(e, context.Request.Path);

            var result = new Response<string>()
            {
                Code = HttpStatus.ERROR,
                Msg = e.Message
            };

            await context.Response.WriteAsync(result.ToJson());
        }
    }
}
