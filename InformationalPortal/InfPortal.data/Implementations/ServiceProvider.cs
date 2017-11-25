using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.data.Interfaces;
using InfPortal.data.Entities;
using InfPortal.data.ArticleProxy;
namespace InfPortal.data.Implementations
{
    public class ServiceProvider: InfPortal.data.Interfaces.IServiceProvider
    {
        ArticleServiceClient articleClient;
        public ServiceProvider()
        {
            articleClient = new ArticleServiceClient("BasicHttpBinding_IArticleService");
        }
        public List<Article> GetArticles()
        {
            List<Article> articleList = new List<Article>();
            foreach(var item in articleClient.GetArticles())
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

            return articleList;
        }

        public Article GetArticleById(int? id)
        {
            Article article = new Article();
            article.Id = articleClient.GetArticleById(id).Id;
            article.Name = articleClient.GetArticleById(id).Name;
            article.PictureLink=articleClient.GetArticleById(id).PictureLink;
            article.Details.Id = articleClient.GetArticleById(id).Id;
            article.Details.Text = articleClient.GetArticleById(id).Details.Text;
            article.Details.Language = articleClient.GetArticleById(id).Details.Language;
            article.Details.Date = articleClient.GetArticleById(id).Details.Date;
            article.Details.VideoLink = articleClient.GetArticleById(id).Details.VideoLink;
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
    }
}
