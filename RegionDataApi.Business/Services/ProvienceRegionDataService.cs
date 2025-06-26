using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegionDataApi.Data.Entities;
using RegionDataApi.Data.Repositories;
using RegionDataApi.Common.Exceptions;
using RegionDataApi.Common.DTOs;

namespace RegionDataApi.Business.Services
{
    public class ProvienceRegionDataService : IProvienceRegionDataService
    {
        private readonly IProvienceRegionDataRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TuikUrlBuilder _urlBuilder;

        public ProvienceRegionDataService(IProvienceRegionDataRepository repository, IHttpClientFactory httpClientFactory, TuikUrlBuilder urlBuilder)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _urlBuilder = urlBuilder;
        }

        /// <summary>
        /// Dış servis çağrısını HttpClientFactory ile yapar, XML'yi JSON'a dönüştürür.
        /// </summary>
        private async Task<JObject> GetProvienceReportsAsync(int startYear, int endYear, int regionCode)
        {
            //var yearsParam = $"{startYear}:{endYear}";
            //var url = $"http://internal.oag.icisleri.gov.tr/Services/Consumer/Tuik/" +
            //          $"MerkeziDagitimSistemiServisi/getNUTSReports?" +
            //          $"languageCode=TR&indicatorId=ADNKS-GK137473-O29001&" +
            //          $"years={yearsParam}&regionTypeCode=3:3&regionCode={regionCode}";

            var url = _urlBuilder.BuildProvienceUrl(startYear, endYear, regionCode);

            var client = _httpClientFactory.CreateClient();
            var xmlText = await client.GetStringAsync(url);

            // XML → JSON çevir
            var doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlText);
            var jsonText = JsonConvert.SerializeXmlNode(doc);

            return JObject.Parse(jsonText);
        }

        public async Task SyncProvienceRegionDataAsync(int startYear, int endYear, int regionCode)
        {
         
            var reportsObj = await GetProvienceReportsAsync(startYear, endYear, regionCode);

            var status = reportsObj["reports"]?["status"]?.Value<int>() ?? 0;

            if (status == -1) 
            {
                var description = reportsObj["reports"]?["description"]?.ToString() ?? "Bilinmeyen hata";
                throw new TuikException($"TUİK servis hatası: {description}", 422);
            }


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

        public async Task<RegionDataDto> GetLatestProvienceByRegionCodeAsync(int regionCode)
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
