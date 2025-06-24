using Microsoft.EntityFrameworkCore;
using RegionDataApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tbl_ProvienceRegionData> Tbl_ProvienceRegionData { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
    
}
