using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.common.Logs
{
    public interface ILoger
    {
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }
}
