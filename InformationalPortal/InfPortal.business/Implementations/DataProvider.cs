using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.business.Interfaces;
using InfPortal.business.DTO;
using InfPortal.data.Interfaces;
using InfPortal.data.Entities;
using InfPortal.data.Implementations;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
namespace InfPortal.business.Implementations
{
    public class DataProvider : IDataProvider
    {
        readonly InfPortal.data.Interfaces.IServiceProvider serviceProvider;

        public DataProvider(InfPortal.data.Interfaces.IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public void AddArticle(ArticleDTO article)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(ArticleDTO article)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(int? id)
        {
            throw new NotImplementedException();
        }

        public ArticleDTO GetArticle(int? id)
        {
            ArticleDTO articleDTO = new ArticleDTO();
            Article article = serviceProvider.GetArticleById(id);
            articleDTO.Id = article.Id;
            articleDTO.Name = article.Name;
            articleDTO.PictureLink = article.PictureLink;
            articleDTO.Details = new InfoDTO()
            {
                Id = article.Details.Id,
                Text = article.Details.Text,
                Date = article.Details.Date,
                Language = article.Details.Language,
                VideoLink = article.Details.VideoLink
            };
            return articleDTO;
        }

        public List<ArticleDTO> GetArticles()
        {
            List<ArticleDTO> articleDTOList = new List<ArticleDTO>();
            try
            {
                foreach (var item in serviceProvider.GetArticles())
                {
                    articleDTOList.Add(new ArticleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        Details = new InfoDTO()
                        {
                            Id = item.Id,
                            Text = item.Details.Text,
                            Date = item.Details.Date,
                            Language = item.Details.Language,
                            VideoLink = item.Details.VideoLink
                        }
                    });
                }
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }                
            return articleDTOList;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int GetCountOfArticles()
        {
            throw new NotImplementedException();
        }


        public List<HeadingDTO> GetHeadings()
        {
            List<HeadingDTO> headingDTOList = new List<HeadingDTO>();
            foreach(var item in serviceProvider.GetHeadings())
            {
                headingDTOList.Add(new HeadingDTO()
                {
                    Id=item.Id,
                    Name=item.Name,
                    Description=item.Description
                });
            }

            return headingDTOList;
        }
    }
}
