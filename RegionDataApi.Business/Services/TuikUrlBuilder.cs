using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Business.Services
{
    public class TuikUrlBuilder
    {
        private readonly string _baseProvienceUrl;
        private readonly string _baseSubProvienceUrl;

        private readonly string _provienceEndpoint;
        private readonly string _subProvienceEndpoint;


        private readonly string _provienceDefaultParams;
        private readonly string _subProvienceDefaultParams;

        public TuikUrlBuilder(IConfiguration configuration)
        {
            _baseProvienceUrl = configuration["TuikService:Provience:BaseUrl"];
            _baseSubProvienceUrl = configuration["TuikService:SubProvience:BaseUrl"];


            _provienceEndpoint = configuration["TuikService:Provience:Endpoint"];
            _subProvienceEndpoint = configuration["TuikService:SubProvience:Endpoint"];


            _provienceDefaultParams = configuration["TuikService:Provience:DefaultParams"];
            _subProvienceDefaultParams = configuration["TuikService:SubProvience:DefaultParams"];
        }

        public string BuildProvienceUrl(int startYear, int endYear, int regionCode)
        {
            var yearsParam = $"{startYear}:{endYear}";
            return $"{_baseProvienceUrl}{_provienceEndpoint}?{_provienceDefaultParams}&years={yearsParam}&regionTypeCode=3:3&regionCode={regionCode}";
        }

        public string BuildSubProvienceUrl(int year, int regionCode)
        {

            return $"{_baseSubProvienceUrl}{_subProvienceEndpoint}?{_subProvienceDefaultParams}&year={year}&provienceCode={regionCode}";
        }
    }

}
