using RegionDataApi.Business.DTOs;
using RegionDataApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Business.Services
{
    public interface IProvienceRegionDataService
    {
        Task SaveProvienceRegionDataAsync(ProvienceRegionDataDto dto);
        Task<ProvienceRegionDataDto> GetLatestProvienceByRegionCodeAsync(int regionCode);
        /// <summary>
        /// Belirtilen yıllar ve bölge kodu için dış servisten çekilen raporları 
        /// parse eder ve veritabanına kaydeder.
        /// </summary>
        Task SyncProvienceRegionDataAsync(int startYear, int endYear, int regionCode);
    }
}
