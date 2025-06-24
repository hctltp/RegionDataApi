using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Data.Entities
{
    public class Tbl_ProvienceRegionData
    {
        public int Id { get; set; }
        public int RegionCode { get; set; }
        public int DataValue { get; set; }
        public DateTime RequestDate { get; set; }
        public string IndicatorId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public int Month { get; set; }
    }
}
