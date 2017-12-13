using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.data.Interfaces;
using InfPortal.data.Entities;
using InfPortal.data.ArticleProxy;
using InfPortal.data.HeadingProxy;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using System.ServiceModel;
using InfPortal.data.Implementations;
namespace InfPortal.data.Implementations
{
    public class ServiceProvider: InfPortal.data.Interfaces.IServiceProvider
    {
        ArticleServiceClient articleClient;
        HeadingServiceClient headingClient;
        const string errorArgument = "Parametr is invalid";
        public ServiceProvider()
        {
            articleClient = new ArticleServiceClient("BasicHttpBinding_IArticleService");
            headingClient = new HeadingServiceClient("BasicHttpBinding_IHeadingService");
        }
        public Article[] GetArticles()
        {            
            var articleList = new List<Article>();
            try
            {
                foreach (var item in articleClient.GetArticles())
                {
                    articleList.Add(new Article()
                    {
                        
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        Details = new Info()
                        {
                            Id = item.Id,
                            Text = item.Details.Text,
                            Language = item.Details.Language,
                            Date = item.Details.Date,
                            VideoLink = item.Details.VideoLink
                        } ,
                        AuthorId=item.AuthorId,
                        AuthorName=item.AuthorName
                    });

                }
            }
            catch(FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            
            return articleList.ToArray<Article>();
        }

        public Article GetArticleById(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var article = new Article();            
            try
            {
                var articleEntity = articleClient.GetArticleById(parsedId);
                    if(articleEntity.Id!=0)
                    {
                        article.Id = articleEntity.Id;
                        article.Name = articleEntity.Name;
                        article.PictureLink = articleEntity.PictureLink;
                        article.Details = new Info() 
                        {
                            Id = articleEntity.Id,
                            Text = articleEntity.Details.Text,
                            Language=articleEntity.Details.Language,
                            Date=articleEntity.Details.Date,
                            VideoLink=articleEntity.Details.VideoLink
                        };
                        article.AuthorId = articleEntity.AuthorId;
                        article.AuthorName = articleEntity.AuthorName;
                    }
                
            }
            catch(FaultException<InfPortal.data.ArticleProxy.ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return article;
        }
        public Article[] GetArticlesByHeadingId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var articleList = new List<Article>();
            try
            {
                foreach (var item in articleClient.GetArticlesByHeadingId(parsedId))
                {
                    articleList.Add(new Article()
                    {

                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,  
                        AuthorId=item.AuthorId,
                        AuthorName=item.AuthorName
                    });

                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return articleList.ToArray<Article>();

        }

        public Heading[] GetHeadings()
        {
            var headingList = new List<Heading>();
            try
            {
                foreach (var item in headingClient.GetHeadings())
                {
                    headingList.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return headingList.ToArray<Heading>();
        }    


        public void AddArticle(Article item)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Article item)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(int? id)
        {
            throw new NotImplementedException();
        }

        public int GetCountOfArticles()
        {
            throw new NotImplementedException();
        }
        
        public Article[] GetArticlesByUserId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var articleList = new List<Article>();
            try
            {
                foreach (ArticleEntity item in articleClient.GetArticlesByUserId(parsedId))
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

            return articleList.ToArray<Article>();
        }


        public Article[] GetArticlesByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articleList = new List<Article>();
                try
                {
                    foreach (ArticleEntity item in articleClient.GetArticleByName(name))
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

                return articleList.ToArray<Article>();
            }
            return null;
        }

        public string[] GetArticleNamesByInput(string name)
        {           
            if(!string.IsNullOrEmpty(name))
            {
                var names = articleClient.GetArticleNamesByInput(name);
                return names;
            }
            return null;
        }
    }
}
