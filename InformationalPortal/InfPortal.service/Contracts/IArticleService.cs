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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IArticleEntityService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IArticleService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        List<ArticleEntity> GetArticles();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ArticleEntity GetArticleById(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        List<ArticleEntity> GetArticlesByHeadingId(int? Id);

        [OperationContract]
        int GetCountOfArticles();
    }
}
