using Microsoft.AspNetCore.Mvc;
using RegionDataApi.Business;
using RegionDataApi.Business.Services;
using System.Net;

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

        [HttpGet("sync-provience/{startYear}/{endYear}/{regionCode}")]
        public async Task<IActionResult> SyncProvience(int startYear, int endYear, int regionCode)
        {
            try
            {
                await _provienceRegionDataService.SyncProvienceRegionDataAsync(startYear, endYear, regionCode);
                return Ok("Province data sync completed.");
            }
            catch (TuikException ex)
            {
                if(ex.Status == 422)
                {
                    return UnprocessableEntity(new { error = ex.Message, StatusCode = ex.Status });
                }
                
                return BadRequest(new { error = ex.Message, StatusCode = ex.Status });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Sunucu hatası oluştu.", details = ex.Message });
            }
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
