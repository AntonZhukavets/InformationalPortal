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
namespace InfPortal.data.Implementations
{
    public class ServiceProvider: InfPortal.data.Interfaces.IServiceProvider
    {
        ArticleServiceClient articleClient;
        HeadingServiceClient headingClient;        
        public ServiceProvider()
        {
            articleClient = new ArticleServiceClient("BasicHttpBinding_IArticleService");
            headingClient = new HeadingServiceClient("BasicHttpBinding_IHeadingService");
        }
        public List<Article> GetArticles()
        {            
            List<Article> articleList = new List<Article>();
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
                        }
                    });

                }
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            
            return articleList;
        }

        public Article GetArticleById(int? id)
        {
            Article article = new Article();
            ArticleEntity articleEntity = articleClient.GetArticleById(id);
            try
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
                
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return article;
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


        public List<Heading> GetHeadings()
        {
            List<Heading> headingList = new List<Heading>();
            foreach(var item in headingClient.GetHeadings())
            {
                headingList.Add(new Heading()
                {
                    Id=item.Id,
                    Name=item.Name,
                    Description=item.Description
                });
            }
            return headingList;
        }
    }
}
