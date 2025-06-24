using RegionDataApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Data.Repositories
{
    public interface IProvienceRegionDataRepository
    {
        Task AddProvienceRegionDataAsync(Tbl_ProvienceRegionData data);
        Task<Tbl_ProvienceRegionData?> GetLatestProvienceByRegionCodeAsync(int regionCode);
    }
}
