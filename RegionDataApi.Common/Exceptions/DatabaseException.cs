using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionDataApi.Common.Exceptions
{
    public class DatabaseException : GeneralException
    {
        public DatabaseException(string message, int status = 500) : base(message, status) { }
    }

}
