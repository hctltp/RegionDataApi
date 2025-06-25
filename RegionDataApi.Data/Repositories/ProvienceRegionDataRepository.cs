using Microsoft.EntityFrameworkCore;
using RegionDataApi.Data.Entities;


namespace RegionDataApi.Data.Repositories
{
    public class ProvienceRegionDataRepository : IProvienceRegionDataRepository
    {
        private readonly AppDbContext _context;

        public ProvienceRegionDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProvienceRegionDataAsync(Tbl_RegionData data)
        {
            _context.Tbl_RegionData.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Tbl_RegionData?> GetLatestProvienceByRegionCodeAsync(int regionCode)
        {
            return await _context.Tbl_RegionData
                .Where(r => r.RegionCode == regionCode)
                .OrderByDescending(r => r.RequestDate)
                .FirstOrDefaultAsync();
        }
    }
}
