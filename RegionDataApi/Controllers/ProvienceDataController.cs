using Microsoft.AspNetCore.Mvc;
using RegionDataApi.Business.Services;

namespace RegionDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvienceDataController : ControllerBase
    {
        private readonly IProvienceRegionDataService _provienceRegionDataService;
        public ProvienceDataController(IProvienceRegionDataService provienceRegionDataService)
        {
            _provienceRegionDataService = provienceRegionDataService;
        }

        [HttpPost("sync-provience/{startYear}/{endYear}/{regionCode}")]
        public async Task<IActionResult> SyncProvience(int startYear, int endYear, int regionCode)
        {
            await _provienceRegionDataService.SyncProvienceRegionDataAsync(startYear, endYear, regionCode);
            return Ok("Provience data sync completed.");
        }

        //[HttpPost("save")]
        //public async Task<IActionResult> SaveProvienceRegionData([FromBody] ProvienceRegionDataDto dto)
        //{
        //    if (dto == null)
        //    {
        //        return BadRequest("Data cannot be null");
        //    }
        //    await _provienceRegionDataService.SaveProvienceRegionDataAsync(dto);
        //    return Ok();
        //}
        [HttpGet("latest/{regionCode}")]
        public async Task<IActionResult> GetLatestProvienceByRegionCode(int regionCode)
        {
            var data = await _provienceRegionDataService.GetLatestProvienceByRegionCodeAsync(regionCode);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

    }
}
