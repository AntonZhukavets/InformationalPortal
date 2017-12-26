using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using InfPortal.data.ArticleProxy;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;

namespace InfPortal.data.Repository
{
    internal class ArticleRepository : IArticleRepository
    {
        ArticleServiceClient articleClient;
        const string errorArgument = "Parametr is invalid";

        public ArticleRepository()
        {
            articleClient = new ArticleServiceClient("BasicHttpBinding_IArticleService");           
        }

        public Article[] GetArticlesPreView()
        {
            var articleList = new List<Article>();
            try
            {
                ArticleEntity[] articleEntities = articleClient.GetArticlesPreView();
                if (articleEntities == null)
                {
                    return null;
                }
                foreach (var item in articleEntities)
                {
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName
                    });

                }
            }
            catch (FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return articleList.ToArray<Article>();
        }

        public Article GetFullArticleById(int? id)
        {

            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            var article = new Article();
            try
            {
                var articleEntity = articleClient.GetFullArticleById(id);                
                if (articleEntity == null || articleEntity.Headings==null)
                {
                    return null;
                }
                var headings = new List<Heading>();                
                foreach (var item in articleEntity.Headings)
                {
                    headings.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
                var articleLinks = new List<ArticleLink>();
                if (articleEntity.ArticleLinks != null)
                {
                    foreach (var item in articleEntity.ArticleLinks)
                    {
                        articleLinks.Add(new ArticleLink()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                article.Id = articleEntity.Id;
                article.Name = articleEntity.Name;
                article.PictureLink = articleEntity.PictureLink;
                article.Details = new Info()
                {
                    Id = articleEntity.Id,
                    Text = articleEntity.Details.Text,
                    Language = new Language()
                    {
                        LanguageId=articleEntity.Details.Language.LanguageId,
                        LanguageName=articleEntity.Details.Language.LanguageName
                    },
                    Date = articleEntity.Details.Date,
                    VideoLink = articleEntity.Details.VideoLink
                };
                article.AuthorId = articleEntity.AuthorId;
                article.AuthorName = articleEntity.AuthorName;
                article.Headings = headings.ToArray<Heading>();
                article.ArticleLinks = articleLinks.ToArray<ArticleLink>();

            }
            catch (FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return article;
        }
        public Article[] GetArticlesPreViewByHeadingId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            var articleList = new List<Article>();
            try
            {
                ArticleEntity[] articleEntities = articleClient.GetArticlesPreViewByHeadingId(id);
                if (articleEntities == null)
                {
                    return null;
                }
                foreach (var item in articleEntities)
                {
                    articleList.Add(new Article()
                    {

                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName
                    });

                }
            }
            catch (FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return articleList.ToArray<Article>();
        }

        public Article[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id)
        {
            if (!id.HasValue || dateFrom==null || dateTo==null)
            {
                throw new ArgumentException(errorArgument);
            }
            var articleList = new List<Article>();
            try
            {
                ArticleEntity[] articleEntities = articleClient.GetArticlesPreViewByHeadingIdAndDatePeriod(dateFrom, dateTo, id);
                if (articleEntities == null)
                {
                    return null;
                }
                foreach (var item in articleEntities)
                {
                    articleList.Add(new Article()
                    {

                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName
                    });

                }
            }
            catch (FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return articleList.ToArray<Article>();
        }
        public bool AddArticle(Article article)
        {
            bool resultOfOperation = false;
            if (article != null && article.Headings != null)
            {
                var headings = new List<ArticleProxy.HeadingEntity>();
                foreach (var item in article.Headings)
                {
                    headings.Add(new ArticleProxy.HeadingEntity()
                    {
                        Id = item.Id
                    });
                }
                try
                {
                    resultOfOperation = articleClient.AddArticle(new ArticleEntity()
                    {
                        Name = article.Name,
                        PictureLink = article.PictureLink,
                        AuthorId = article.AuthorId,
                        Details = new InfoEntity()
                        {
                            Text = article.Details.Text,
                            Date = article.Details.Date,
                            Language = new LanguageEntity() 
                            {
                                LanguageId = article.Details.Language.LanguageId                                
                            },
                            VideoLink = article.Details.VideoLink
                        },
                        Headings = headings.ToArray<ArticleProxy.HeadingEntity>()
                    });
                }
                catch (FaultException<ArticleProxy.ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            return resultOfOperation;
        }

        public bool EditArticle(Article article)
        {
            bool resultOfOperation = false;
            if (article != null && article.Headings != null)
            {
                var headings = new List<ArticleProxy.HeadingEntity>();
                foreach (var item in article.Headings)
                {
                    headings.Add(new ArticleProxy.HeadingEntity()
                    {
                        Id = item.Id
                    });
                }
                try
                {
                    resultOfOperation = articleClient.EditArticle(new ArticleEntity()
                    {
                        Id = article.Id,
                        Name = article.Name,
                        PictureLink = article.PictureLink,
                        Details = new InfoEntity()
                        {
                            Text = article.Details.Text,
                            Date = article.Details.Date,
                            Language = new LanguageEntity()
                            {
                                LanguageId=article.Details.Language.LanguageId
                            },
                            VideoLink = article.Details.VideoLink
                        },
                        Headings = headings.ToArray<ArticleProxy.HeadingEntity>()
                    });
                }
                catch (FaultException<ArticleProxy.ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Detail.Message);
                }
            }
            return resultOfOperation;
        }

        public bool DeleteArticle(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = articleClient.DeleteArticle(id);
                }
                catch (FaultException<ArticleProxy.ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Detail.Message);
                }
            }
            return resultOfOperation;
        }
        public Article[] GetArticlesPreViewByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            var articleList = new List<Article>();
            try
            {
                ArticleEntity[] articleEntities = articleClient.GetArticlesPreViewByUserId(id);
                if (articleEntities == null)
                {
                    return null;
                }
                foreach (ArticleEntity item in articleEntities)
                {
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName
                    });

                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return articleList.ToArray<Article>();
        }


        public Article[] GetArticlesPreViewByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articleList = new List<Article>();
                try
                {
                    ArticleEntity[] articleEntities = articleClient.GetArticlesPreViewByName(name);
                    if (articleEntities == null)
                    {
                        return null;
                    }
                    foreach (ArticleEntity item in articleEntities)
                    {
                        articleList.Add(new Article()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PictureLink = item.PictureLink,
                            AuthorId = item.AuthorId,
                            AuthorName = item.AuthorName
                        });

                    }
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Message);
                }

                return articleList.ToArray<Article>();
            }
            return null;
        }

    }
}
