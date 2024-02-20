using SqlSugar;

namespace Wms.Web.Api.Service.SqlSugarMain
{
    public static class SqlsugarSetup
    {
        public static void AddSqlSugar(this IServiceCollection services, IConfiguration configuration, string dbName = "ConnectionStrings:ConnectionString")
        {
            SqlSugarScope sqlSugar = new(new ConnectionConfig()
            { 
                DbType = SqlSugar.DbType.SqlServer,
                ConnectionString = configuration[dbName],
                IsAutoCloseConnection = true,
            });

            services.AddSingleton<ISqlSugarClient>(sqlSugar);
        }
    }
}
