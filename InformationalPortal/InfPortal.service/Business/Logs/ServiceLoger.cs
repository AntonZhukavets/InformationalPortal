using System;
using log4net;
using log4net.Config;

namespace InfPortal.service.Business.Logs
{
    public class ServiceLoger : IServiceLoger
    {
        private readonly ILog log;
        public ServiceLoger()
        {
            log = LogManager.GetLogger("SERVICELOGGER");
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