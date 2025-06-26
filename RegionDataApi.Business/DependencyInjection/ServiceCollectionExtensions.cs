using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegionDataApi.Business.Services;
using RegionDataApi.Data;
using RegionDataApi.Data.Repositories;
using RegionDataApi.Business.DTOs;        // TuikServiceOptions için

namespace RegionDataApi.Business.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegionDataBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // HttpClient
            services.AddHttpClient();

            // Repositories
            services.AddScoped<IProvienceRegionDataRepository, ProvienceRegionDataRepository>();

            // Services
            services.AddScoped<IProvienceRegionDataService, ProvienceRegionDataService>();
            services.AddScoped<ISubProvienceRegionDataService, SubProvienceRegionDataService>();

            
            
            services.AddTransient<TuikUrlBuilder>();

            return services;
        }
    }
}
