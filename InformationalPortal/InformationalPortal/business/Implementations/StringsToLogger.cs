using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationalPortal.business.Implementations
{
    public static class StringsToLogger
    {
        public const string  exceptionToLogger="Exception in metod {0}. Details: {1}";
        public const string invalidParametrToLogger = "Ivalid parametr in metod {0}.";
        public const string succesfullResultToLogger = "";
        public const string accessDeniedToLogger = "Access denied to method {0} with login {1}";
    }
}