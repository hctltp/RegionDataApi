using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Common.Exceptions
{
    public class TuikException : GeneralException
    {
        public TuikException(string message, int status = 400) : base(message, status) { }
    }

}
