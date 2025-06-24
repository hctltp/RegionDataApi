using Microsoft.EntityFrameworkCore;
using RegionDataApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Data.Repositories
{
    public class ProvienceRegionDataRepository : IProvienceRegionDataRepository
    {
        private readonly AppDbContext _context;

        public ProvienceRegionDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProvienceRegionDataAsync(Tbl_ProvienceRegionData data)
        {
            _context.Tbl_ProvienceRegionData.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Tbl_ProvienceRegionData?> GetLatestProvienceByRegionCodeAsync(int regionCode)
        {
            return await _context.Tbl_ProvienceRegionData
                .Where(r => r.RegionCode == regionCode)
                .OrderByDescending(r => r.RequestDate)
                .FirstOrDefaultAsync();
        }
    }
}
