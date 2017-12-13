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
    [ServiceContract]
    public interface IArticleService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ArticleEntity[] GetArticles();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity GetArticleById(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticleByName(string name);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        string[] GetArticleNamesByInput(string name);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesByHeadingId(int? Id);

        [OperationContract]
        int GetCountOfArticles();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesByUserId(int? id);
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool AddArticle(ArticleEntity article);
    }
}
