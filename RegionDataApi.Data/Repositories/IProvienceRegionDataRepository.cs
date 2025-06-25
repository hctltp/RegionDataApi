using RegionDataApi.Data.Entities;


namespace RegionDataApi.Data.Repositories
{
    public interface IProvienceRegionDataRepository
    {
        Task AddProvienceRegionDataAsync(Tbl_RegionData data);
        Task<Tbl_RegionData?> GetLatestProvienceByRegionCodeAsync(int regionCode);
    }
}
