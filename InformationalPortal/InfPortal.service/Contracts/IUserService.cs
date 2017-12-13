using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InfPortal.service.Entities;
using InfPortal.service.Implementations;
namespace InfPortal.service.Contracts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IUserService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IUserService
    {
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        void Register(UserEntity user);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        bool IsValidUser(string login, string password);
       
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        UserEntity GetUserByLogin(string login);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        int GetUserIdByLogin(string login);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        void UpdateUser(UserEntity user);
       
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        UserEntity GetUserById(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool DeleteUserById(int? id);

    }
}
