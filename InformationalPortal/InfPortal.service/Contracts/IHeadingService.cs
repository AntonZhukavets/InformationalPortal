using System.Runtime.Serialization;
using System.ServiceModel;
using InfPortal.service.Entities;
using InfPortal.service.Implementations;
using System;

namespace InfPortal.service.Contracts
{
  
    [ServiceContract]
    public interface IHeadingService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        HeadingEntity[] GetHeadings();
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        HeadingEntity[] GetHeadingsByArticleId(int? id);
    }
}
