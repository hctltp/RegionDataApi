using Microsoft.EntityFrameworkCore;
using RegionDataApi.Data.Entities;


namespace RegionDataApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tbl_RegionData> Tbl_RegionData { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
    
}
