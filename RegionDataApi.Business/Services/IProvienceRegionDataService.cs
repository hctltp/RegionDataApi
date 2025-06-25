using RegionDataApi.Business.DTOs;


namespace RegionDataApi.Business.Services
{
    public interface IProvienceRegionDataService
    {
        Task SaveProvienceRegionDataAsync(RegionDataDto dto);
        Task<RegionDataDto> GetLatestProvienceByRegionCodeAsync(int regionCode);
        /// <summary>
        /// Belirtilen yıllar ve bölge kodu için dış servisten çekilen raporları 
        /// parse eder ve veritabanına kaydeder.
        /// </summary>
        Task SyncProvienceRegionDataAsync(int startYear, int endYear, int regionCode);
    }
}
