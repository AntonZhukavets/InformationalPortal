using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
namespace InfPortal.common.Logs
{
    public static class Logger
    {
        private static ILog log;

        static Logger()
        {
            log = LogManager.GetLogger("LOGGER");
        }
        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
        public static void AddToLog(string type, string message)
        {            
            switch (type)
            {
                case  "info":
                    log.Info(message);
                    break;
                case "error":
                    log.Error(message);
                    break;
                case "warn":
                    log.Warn(message);
                    break;
                case "fatal":
                    log.Fatal(message);
                    break;

            }
            
        }
    }
}
