using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Business
{
    public class TuikException : Exception
    {
        public int Status { get; }

        public TuikException(string message, int status) : base(message)
        {
            Status = status;
        }
    }
}
