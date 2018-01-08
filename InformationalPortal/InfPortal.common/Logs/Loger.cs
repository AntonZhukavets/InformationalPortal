using System;
using log4net;
using log4net.Config;

namespace InfPortal.common.Logs
{
    public class Loger: ILoger
    {
        private ILog log;

        public Loger()
        {
           
            
                log = LogManager.GetLogger("LOGGER");
           
            
            XmlConfigurator.Configure();
        }
        public void Info(string message)
        {
            log.Info(message);
        }        
        public void Error(string message)
        {
            log.Error(message);
        }
        public void Warning(string message)
        {
            log.Warn(message);
        }
    }
}
