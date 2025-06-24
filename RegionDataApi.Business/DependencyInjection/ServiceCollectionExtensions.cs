using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegionDataApi.Business.Services;
using RegionDataApi.Data;
using RegionDataApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Business.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegionDataBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient();

            services.AddScoped<IProvienceRegionDataRepository, ProvienceRegionDataRepository>();
            services.AddScoped<IProvienceRegionDataService, ProvienceRegionDataService>();

            return services;
        }
    }
}
