using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.common.Exceptions
{
    public class DataBaseConnectionException : Exception
    {

        public DataBaseConnectionException(string message)
            : base(message)
        { }
            
        
    }
}
