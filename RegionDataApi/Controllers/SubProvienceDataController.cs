using Microsoft.AspNetCore.Mvc;
using RegionDataApi.Business.Services;
using RegionDataApi.Common.Exceptions;


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

        [HttpGet("sync-provience/{year}/{regionCode}")]
        public async Task<IActionResult> SyncSubProvience(int year, int regionCode)
        {
            try
            {
                await _subProvienceRegionDataService.SyncSubProvienceRegionDataAsync(year, regionCode);
                return Ok("Senkronizasyon Başarılı!");
            }
            catch (TuikException ex)
            {
                if (ex.Status == 422)
                {
                    return UnprocessableEntity(new { error = ex.Message, StatusCode = ex.Status });
                }

                return BadRequest(new { error = ex.Message, StatusCode = ex.Status });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, new { error = "Veritabanı hatası oluştu.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Sunucu hatası oluştu.", details = ex.Message });
            }
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

        [HttpGet("sync-provience-auto")]
        public async Task<IActionResult> SyncSubProvienceAuto()
        {
            try
            {
                int year = DateTime.UtcNow.Year;
                int regionCode = 0;
                // TODO: Burada regionCode parametresinin önemi vardır.Mutlaka bir değer set edilmesi gereklidir
                // Tüm iller için 81 kez döngüyle çağırmak mı gereklidir?
                await _subProvienceRegionDataService.SyncSubProvienceRegionDataAsync(year, regionCode);
                return Ok("Senkronizasyon Başarılı!");
            }
            catch (TuikException ex)
            {
                if (ex.Status == 422)
                {
                    return UnprocessableEntity(new { error = ex.Message, StatusCode = ex.Status });
                }

                return BadRequest(new { error = ex.Message, StatusCode = ex.Status });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, new { error = "Veritabanı hatası oluştu.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Sunucu hatası oluştu.", details = ex.Message });
            }
        }

    }
}
