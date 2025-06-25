using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegionDataApi.Business.Services;
using RegionDataApi.Data;


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
            services.AddScoped<ISubProvienceRegionDataService, SubProvienceRegionDataService>();

            return services;
        }
    }
}
