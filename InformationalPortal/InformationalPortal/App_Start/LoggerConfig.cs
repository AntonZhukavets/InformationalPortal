using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfPortal.common.Logs;

namespace InformationalPortal.App_Start
{
    public class LoggerConfig
    {
        public static void RegistrLoger()
        {
            Logger.InitLogger();
        }
        
    }
}