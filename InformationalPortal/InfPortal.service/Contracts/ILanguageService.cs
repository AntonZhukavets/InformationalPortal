using System;
using System.ServiceModel;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;

namespace InfPortal.service.Contracts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ILanguageService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ILanguageService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        LanguageEntity[] GetLanguages();        

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool AddLanguage(LanguageEntity language);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool RestoreLanguage(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool DeleteLanguage(int? id);
    }
}
