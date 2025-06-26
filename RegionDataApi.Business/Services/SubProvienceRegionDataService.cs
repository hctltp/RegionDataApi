using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegionDataApi.Business.DTOs;
using RegionDataApi.Data.Entities;
using RegionDataApi.Data.Repositories;


namespace RegionDataApi.Business.Services
{
    public class SubProvienceRegionDataService : ISubProvienceRegionDataService
    {
        private readonly IProvienceRegionDataRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TuikUrlBuilder _urlBuilder;
        public SubProvienceRegionDataService(IProvienceRegionDataRepository repository, IHttpClientFactory httpClientFactory, TuikUrlBuilder urlBuilder)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _urlBuilder = urlBuilder;
        }


        private async Task<JObject> GetSubProvienceReportsAsync(int year, int regionCode)
        {
            //var url = $"http://internal.oag.icisleri.gov.tr/Services/Consumer/Tuik/" +
            //    $"MerkeziDagitimSistemiServisi/getSubProvienceReports?languageCode=TR&" +
            //    $"indicatorId=ADNKS-GK137473-O29001&year={year}&provienceCode={regionCode}";
            var url = _urlBuilder.BuildSubProvienceUrl(year, regionCode);

            var client = _httpClientFactory.CreateClient();
            var xmlText = await client.GetStringAsync(url);

            // XML → JSON çevir
            var doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlText);
            var jsonText = JsonConvert.SerializeXmlNode(doc);

            return JObject.Parse(jsonText);
        }

        public async Task SyncSubProvienceRegionDataAsync(int year, int regionCode)
        {

            var reportsObj = await GetSubProvienceReportsAsync(year, regionCode);


            var reports = reportsObj["reports"]["report"]
                              .Children()
                              .Select(r => new RegionDataDto
                              {
                                  RegionCode = (int)r["regionCode"],
                                  RegionTypeCode = (int)r["regionTypeCode"],
                                  DataValue = (int)r["dataValue"],
                                  RequestDate = DateTime.UtcNow,
                                  IndicatorId = (string)r["indicatorId"],
                                  Year = (int)r["year"],
                                  Term = (int)r["term"],
                                  Month = (int)r["month"]
                              });

            // 3) Her bir DTO'yu repository üzerinden kaydet
            foreach (var dto in reports)
            {
                // istersen var olanı kontrol edip güncelleme ya da ekleme yapabilirsin
                await _repository.AddProvienceRegionDataAsync(new Tbl_RegionData
                {
                    RegionCode = dto.RegionCode,
                    RegionTypeCode = dto.RegionTypeCode,
                    DataValue = dto.DataValue,
                    RequestDate = dto.RequestDate,
                    IndicatorId = dto.IndicatorId,
                    Year = dto.Year,
                    Term = dto.Term,
                    Month = dto.Month
                });
            }
        }
        public async Task SaveSubProvienceRegionDataAsync(RegionDataDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Data cannot be null");
            }

            var entity = new Tbl_RegionData
            {
                RegionCode = dto.RegionCode,
                RegionTypeCode = dto.RegionTypeCode,
                DataValue = dto.DataValue,
                RequestDate = dto.RequestDate,
                IndicatorId = dto.IndicatorId,
                Year = dto.Year,
                Term = dto.Term,
                Month = dto.Month
            };

            await _repository.AddProvienceRegionDataAsync(entity);
        }
        public async Task<RegionDataDto> GetLatestSubProvienceByRegionCodeAsync(int regionCode)
        {
            var entity = await _repository.GetLatestProvienceByRegionCodeAsync(regionCode);
            if (entity == null)
            {
                return null;
            }
            return new RegionDataDto
            {
                RegionCode = entity.RegionCode,
                DataValue = entity.DataValue,
                RequestDate = entity.RequestDate,
                IndicatorId = entity.IndicatorId,
                Year = entity.Year,
                Term = entity.Term,
                Month = entity.Month
            };
        }
    }
}



