using RegionDataApi.Common.DTOs;


namespace RegionDataApi.Business.Services
{
    public interface ISubProvienceRegionDataService
    {
        
        Task<RegionDataDto> GetLatestSubProvienceByRegionCodeAsync(int regionCode);

        Task SyncSubProvienceRegionDataAsync(int year, int regionCode);
    }
}
