using System;
using System.ServiceModel;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;


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

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool AddHeading(HeadingEntity heading);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool EditHeading(HeadingEntity heading);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool DeleteHeading(int? id);
    }
}
