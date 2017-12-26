using System.Runtime.Serialization;


namespace InfPortal.service.Business.Exceptions
{
    [DataContract]
    public class ServiceException
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}