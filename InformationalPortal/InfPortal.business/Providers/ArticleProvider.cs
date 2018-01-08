using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.common.Logs;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;

namespace InfPortal.business.Providers
{
    public class ArticleProvider: IArticleProvider
    {
        readonly IArticleRepository articleRepository;
        readonly ICacheProvider cacheProvider;
        const string errorArgument = "Parametr is invalid";
        const string cacheKeyGetFullArticleById = "GetFullArticleById";
        const string cacheKeyGetArticlesPreView = "GetArticlesPreView";
        const string cacheKeyGetArticlesPreViewByHeadingId = "GetArticlesPreViewByHeadingId";
        const string cacheKeyGetHeadings = "GetHeadings";
        const string cacheKeyGetArticlesPreViewByUserId = "GetArticlesPreViewByUserId";
        public ArticleProvider(IArticleRepository articleRepository, ICacheProvider cacheProvider)
        {
            this.articleRepository = articleRepository;
            this.cacheProvider = cacheProvider;            
        }

        public ArticleDTO GetFullArticleById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            if (cacheProvider.Get(cacheKeyGetFullArticleById + id.ToString()) != null)
            {
                return cacheProvider.Get(cacheKeyGetFullArticleById + id.ToString()) as ArticleDTO;
            }
            var articleDTO = new ArticleDTO();
            var headings = new List<HeadingDTO>();
            var articleLinksDTO = new List<ArticleLinkDTO>();
            try
            {
                var article = articleRepository.GetFullArticleById(id);
                if (article == null || article.Headings==null)
                {
                    return null;
                }
                foreach (var item in article.Headings)
                {
                    headings.Add(new HeadingDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
                var articleLinks = article.ArticleLinks;
                if (articleLinks != null)
                {
                    foreach (var item in articleLinks)
                    {
                        articleLinksDTO.Add(new ArticleLinkDTO()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                articleDTO.Id = article.Id;
                articleDTO.Name = article.Name;
                articleDTO.PictureLink = article.PictureLink;
                articleDTO.Picture = article.Picture;
                articleDTO.Details = new InfoDTO()
                {
                    Id = article.Details.Id,
                    Text = article.Details.Text,
                    Date = article.Details.Date,
                    Language = new LanguageDTO() 
                    {
                        LanguageId=article.Details.Language.LanguageId,
                        LanguageName=article.Details.Language.LanguageName
                    },
                    VideoLink = article.Details.VideoLink
                };
                articleDTO.AuthorId = article.AuthorId;
                articleDTO.AuthorName = article.AuthorName;
                articleDTO.Headings = headings.ToArray<HeadingDTO>();
                articleDTO.ArticleLinks = articleLinksDTO.ToArray<ArticleLinkDTO>();
                cacheProvider.Insert(cacheKeyGetFullArticleById + id.ToString(), articleDTO);

            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return articleDTO;
        }


        public ArticleDTO[] GetArticlesPreView()
        {
            if (cacheProvider.Get(cacheKeyGetArticlesPreView) != null)
            {
                return cacheProvider.Get(cacheKeyGetArticlesPreView) as ArticleDTO[];
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                var articles = articleRepository.GetArticlesPreView();
                if (articles == null)
                {
                    return null;
                }
                foreach (var item in articles)
                {
                    articleDTOList.Add(new ArticleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName,
                        Picture=item.Picture
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            cacheProvider.Insert("GetArticlesPreView", articleDTOList.ToArray<ArticleDTO>());
            return articleDTOList.ToArray<ArticleDTO>();
        }


        public ArticleDTO[] GetArticlesPreViewByHeadingId(int? id)
        {

            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                var articles = articleRepository.GetArticlesPreViewByHeadingId(id);
                if (articles == null)
                {
                    return null;
                }
                foreach (var item in articles)
                {
                    articleDTOList.Add(new ArticleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName,
                        Picture = item.Picture
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return articleDTOList.ToArray<ArticleDTO>();
        }
        public ArticleDTO[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id)
        {
            if (!id.HasValue || dateFrom==null || dateTo==null)
            {
                throw new ArgumentException(errorArgument);
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                var articles = articleRepository.GetArticlesPreViewByHeadingIdAndDatePeriod(dateFrom, dateTo, id);
                if (articles == null)
                {
                    return null;
                }
                foreach (var item in articles)
                {
                    articleDTOList.Add(new ArticleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName,
                        Picture = item.Picture
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return articleDTOList.ToArray<ArticleDTO>();
        }
        public bool AddArticle(ArticleDTO article)
        {
            bool resultOfOperation = false;
            if (article != null && article.Headings != null)
            {
                var headings = new List<Heading>();
                foreach (var item in article.Headings)
                {
                    headings.Add(new Heading()
                    {
                        Id = item.Id
                    });
                }
                try
                {
                    resultOfOperation = articleRepository.AddArticle(new Article()
                    {
                        Name = article.Name,
                        PictureLink = article.PictureLink,
                        AuthorId = article.AuthorId,
                        Details = new Info()
                        {
                            Text = article.Details.Text,
                            Language = new Language() 
                            {
                                LanguageId=article.Details.Language.LanguageId
                            },
                            Date = article.Details.Date,
                            VideoLink = article.Details.VideoLink
                        },
                        Headings = headings.ToArray<Heading>(),
                        Picture=article.Picture
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                if (resultOfOperation)
                {
                    cacheProvider.Remove(cacheKeyGetArticlesPreView);
                    cacheProvider.Remove(cacheKeyGetArticlesPreViewByUserId + article.AuthorId.ToString());
                }
            }
            return resultOfOperation;
        }

        public bool EditArticle(ArticleDTO article)
        {
            bool resultOfOperation = false;
            if (article != null && article.Headings != null)
            {
                var headings = new List<Heading>();
                foreach (var item in article.Headings)
                {
                    headings.Add(new Heading()
                    {
                        Id = item.Id
                    });
                }
                try
                {
                    resultOfOperation = articleRepository.EditArticle(new Article()
                    {
                        Id = article.Id,
                        Name = article.Name,
                        PictureLink = article.PictureLink,
                        Details = new Info()
                        {
                            Text = article.Details.Text,
                            Language = new Language()
                            {
                                LanguageId = article.Details.Language.LanguageId
                            },
                            Date = article.Details.Date,
                            VideoLink = article.Details.VideoLink
                        },
                        Headings = headings.ToArray<Heading>(),
                        Picture = article.Picture
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                if (resultOfOperation)
                {
                    cacheProvider.Remove(cacheKeyGetFullArticleById + article.Id.ToString());
                    cacheProvider.Remove(cacheKeyGetArticlesPreView);
                    cacheProvider.Remove(cacheKeyGetArticlesPreViewByUserId + article.AuthorId);
                }
            }
            return resultOfOperation;
        }

        public bool DeleteArticle(int? id)
        {
            bool resultOfOperation = false;
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            try
            {
                resultOfOperation = articleRepository.DeleteArticle(id);
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }            
            if (resultOfOperation)
            {
                cacheProvider.Remove(cacheKeyGetFullArticleById + id.ToString());
                cacheProvider.Remove(cacheKeyGetArticlesPreView);
                cacheProvider.Remove(cacheKeyGetArticlesPreViewByUserId + HttpContext.Current.User.Identity.Name);
            }
            return resultOfOperation;
        }

        public ArticleDTO[] GetArticlesPreViewByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            if (cacheProvider.Get(cacheKeyGetArticlesPreViewByUserId + id.ToString()) != null)
            {
                return cacheProvider.Get(cacheKeyGetArticlesPreViewByUserId + id.ToString()) as ArticleDTO[];
            }
            var articleDTOList = new List<ArticleDTO>();
            try
            {
                Article[] articles = articleRepository.GetArticlesPreViewByUserId(id);
                if (articles == null)
                {
                    return null;
                }
                foreach (var item in articles)
                {
                    articleDTOList.Add(new ArticleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName,
                        Picture = item.Picture
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            cacheProvider.Insert(cacheKeyGetArticlesPreViewByUserId + id.ToString(), articleDTOList.ToArray<ArticleDTO>());
            return articleDTOList.ToArray<ArticleDTO>();
        }


        public ArticleDTO[] GetArticlesPreViewByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var articleDTOList = new List<ArticleDTO>();
                try
                {
                    Article[] articles = articleRepository.GetArticlesPreViewByName(name);
                    if (articles == null)
                    {
                        return null;
                    }
                    foreach (var item in articles)
                    {
                        articleDTOList.Add(new ArticleDTO()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PictureLink = item.PictureLink,
                            AuthorId = item.AuthorId,
                            AuthorName = item.AuthorName,
                            Picture = item.Picture
                        });
                    }
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                return articleDTOList.ToArray<ArticleDTO>();
            }
            return null;
        }


    }
}
