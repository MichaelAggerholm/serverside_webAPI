using ClassLibrary.Content;
using ClassLibrary.DataManager;
using ClassLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["connectionStrings:Web_API_DBConnectionString"];
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly("ASP.NET_Core_Web_API")));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
