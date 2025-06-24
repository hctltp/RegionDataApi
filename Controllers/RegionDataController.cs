using Microsoft.AspNetCore.Mvc;
using RegionDataApi.Business.DTOs;
using RegionDataApi.Business.Services;

namespace RegionDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionDataController : ControllerBase
    {
        private readonly IRegionDataService _service;

        public RegionDataController(IRegionDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegionDataDto dto)
        {
            await _service.SaveRegionDataAsync(dto);
            return Ok("Kayıt başarılı.");
        }

        [HttpGet("latest/{regionCode}")]
        public async Task<IActionResult> GetLatest(int regionCode)
        {
            var dto = await _service.GetLatestDataAsync(regionCode);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }
    }
}