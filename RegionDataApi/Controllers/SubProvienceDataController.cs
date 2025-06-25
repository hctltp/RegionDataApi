using Microsoft.AspNetCore.Mvc;
using RegionDataApi.Business.Services;


namespace RegionDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProvienceDataController : ControllerBase
    {
        private readonly ISubProvienceRegionDataService _subProvienceRegionDataService;
        public SubProvienceDataController(ISubProvienceRegionDataService subProvienceRegionDataService)
        {
            _subProvienceRegionDataService = subProvienceRegionDataService;
        }

        [HttpPost("sync-provience/{year}/{regionCode}")]
        public async Task<IActionResult> SyncSubProvience(int year, int regionCode)
        {
            await _subProvienceRegionDataService.SyncSubProvienceRegionDataAsync(year, regionCode);
            return Ok("Provience data sync completed.");
        }

        [HttpGet("latest/{regionCode}")]
        public async Task<IActionResult> GetLatestProvienceByRegionCode(int regionCode)
        {
            var data = await _subProvienceRegionDataService.GetLatestSubProvienceByRegionCodeAsync(regionCode);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

    }
}
