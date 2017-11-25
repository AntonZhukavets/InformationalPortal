﻿using InfPortal.service.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfPortal.service.Contracts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IArticleService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IArticleService
    {
        [OperationContract]
        List<ArticleEntity> GetArticles();

        [OperationContract]
        ArticleEntity GetArticleById(int? id);

        [OperationContract]
        List<ArticleEntity> GetArticlesByHeadingName(string HeadingName);

        [OperationContract]
        int GetCountOfArticles();
    }
}