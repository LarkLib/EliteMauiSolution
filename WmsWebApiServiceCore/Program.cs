
using System.Text;
using Wms.Web.Api.Service.Filters;
using Wms.Web.Api.Service.SqlSugarMain;

namespace Wms.Web.Api.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiLogFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            builder.Logging.AddLog4Net(@"Configs\log4net.config");

            //SqlSugar×¢Èë
            builder.Services.AddSqlSugar(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}