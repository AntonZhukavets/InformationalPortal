using System;
using System.ServiceModel;
using InfPortal.service.Business.Exceptions;
using InfPortal.service.Entities;


namespace InfPortal.service.Contracts
{
    [ServiceContract]
    public interface IArticleService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ArticleEntity[] GetArticlesPreView();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity GetFullArticleById(int? id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesPreViewByName(string name);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        string[] GetArticleNamesByInput(string name);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesPreViewByHeadingId(int? Id);

        [OperationContract]
        int GetCountOfArticles();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id);
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        ArticleEntity[] GetArticlesPreViewByUserId(int? id);
        
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool AddArticle(ArticleEntity article);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool EditArticle(ArticleEntity article);
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [FaultContract(typeof(ArgumentException))]
        bool DeleteArticle(int? id);
    }
}
