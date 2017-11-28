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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IHeadingService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IHeadingService
    {
        [OperationContract]
        List<HeadingEntity> GetHeadings();
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        List<HeadingEntity> GetHeadingsByArticleId(int? id);
    }
}
