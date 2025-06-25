
namespace RegionDataApi.Data.Entities
{
    public class Tbl_RegionData
    {
        public int Id { get; set; }
        public int RegionCode { get; set; }
        public int RegionTypeCode { get; set; }
        public int DataValue { get; set; }
        public DateTime RequestDate { get; set; }
        public string IndicatorId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public int Month { get; set; }
    }
}
