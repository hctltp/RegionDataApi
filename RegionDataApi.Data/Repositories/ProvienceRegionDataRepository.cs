using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegionDataApi.Common.Exceptions;
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
            try
            {
                _context.Tbl_RegionData.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Veritabanı save/update hatası (unique constraint, foreign key vs.)
                throw new DatabaseException("Veritabanı save/update hatası", 500);
            }
            catch (SqlException ex)
            {
                // SQL Server özel hataları (timeout, bağlantı hatası vs.)
                throw new DatabaseException("Veritabanı genel hata", 500);
            }
            catch (Exception ex)
            {
                // Diğer tüm hatalar
                throw new DatabaseException("Veritabanı diğer hatalar", 500);
            }

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
