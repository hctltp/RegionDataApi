using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Common.Exceptions
{
    public class GeneralException : Exception
    {
        public int Status { get; }

        public GeneralException(string message, int status) : base(message)
        {
            Status = status;
        }
    }
}
