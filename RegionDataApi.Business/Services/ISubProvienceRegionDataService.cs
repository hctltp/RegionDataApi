using RegionDataApi.Business.DTOs;


namespace RegionDataApi.Business.Services
{
    public interface ISubProvienceRegionDataService
    {
        Task SaveSubProvienceRegionDataAsync(RegionDataDto dto);

        Task<RegionDataDto> GetLatestSubProvienceByRegionCodeAsync(int regionCode);

        Task SyncSubProvienceRegionDataAsync(int year, int regionCode);
    }
}
