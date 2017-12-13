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
        const string errorArgument = "Parametr is invalid";
        public DataProvider(InfPortal.data.Interfaces.IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ArticleDTO GetArticle(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var articleDTO = new ArticleDTO();
            try
            {
                var article = serviceProvider.GetArticleById(parsedId);
                if (article.Id != 0)
                {
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
                    articleDTO.AuthorId = article.AuthorId;
                    articleDTO.AuthorName = article.AuthorName;
                }
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return articleDTO;
        }


        public ArticleDTO[] GetArticles()
        {
           var articleDTOList = new List<ArticleDTO>();
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
                        },
                        AuthorId=item.AuthorId,
                        AuthorName=item.AuthorName
                    });
                }
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }                
            return articleDTOList.ToArray<ArticleDTO>();
        }


        public ArticleDTO[] GetArticlesByHeadingId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                foreach (var item in serviceProvider.GetArticlesByHeadingId(parsedId))
                {
                    articleDTOList.Add(new ArticleDTO()
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
            return articleDTOList.ToArray<ArticleDTO>();
        }

        public HeadingDTO[] GetHeadings()
        {
            var headingDTOList = new List<HeadingDTO>();
            try
            {
                foreach (var item in serviceProvider.GetHeadings())
                {
                    headingDTOList.Add(new HeadingDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }

            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return headingDTOList.ToArray<HeadingDTO>();
        }

              

        public int GetCountOfArticles()
        {
            throw new NotImplementedException();
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





        public ArticleDTO[] GetArticlesByUserId(int? id)
        {
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                foreach (var item in serviceProvider.GetArticlesByUserId(parsedId))
                {
                    articleDTOList.Add(new ArticleDTO()
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
            return articleDTOList.ToArray<ArticleDTO>();
        }


        public ArticleDTO[] GetArticlesByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articleDTOList = new List<ArticleDTO>();
                try
                {
                    foreach (var item in serviceProvider.GetArticlesByName(name))
                    {
                        articleDTOList.Add(new ArticleDTO()
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
                return articleDTOList.ToArray<ArticleDTO>();
            }
            return null;
        }

        public string[] GetArticleNamesByInput(string name)
        {            
            if (!string.IsNullOrEmpty(name))
            {                
                try
                {
                    var names = serviceProvider.GetArticleNamesByInput(name);
                    return names;
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
               
            }
            return null;
        }
    }
}
