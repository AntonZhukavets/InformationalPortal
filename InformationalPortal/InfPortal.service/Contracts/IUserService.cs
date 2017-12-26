using System;
using System.ServiceModel;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;


namespace InfPortal.service.Contracts
{    
    [ServiceContract]
    public interface IUserService
    {
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool Register(UserEntity user);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool IsValidUser(string login, string password);
       
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        UserEntity GetUserByLoginAndPassword(string login, string password);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool UpdateUser(UserEntity user);
       
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        UserEntity GetUserById(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool DeleteUser(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool ResumeUser(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool MakeAdmin(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        UserEntity[] GetAllUsers();
    }
}
