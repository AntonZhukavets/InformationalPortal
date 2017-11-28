using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace InfPortal.service.Implementations
{
    [DataContract]
    public class ServiceException
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}