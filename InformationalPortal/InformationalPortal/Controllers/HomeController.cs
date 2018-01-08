using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.business.Implementations;
using InformationalPortal.Models;
using InfPortal.business.DTO;
using InfPortal.business.Providers;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.common.Logs;


namespace InformationalPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleProvider articleProvider;
        private readonly IHeadingProvider headingProvider;
        private readonly ILanguageProvider languageProvider;
        private readonly ILoger loger;
        private const string errorMessage = "Parametr is invalid";
        public HomeController(IArticleProvider articleProvider, IHeadingProvider headingProvider, ILanguageProvider languageProvider, ILoger loger)
        {           
            this.articleProvider = articleProvider;
            this.headingProvider = headingProvider;
            this.languageProvider = languageProvider;
            this.loger = loger;            
        }

        [HttpGet]
        public ActionResult Index()
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            var articleList = new List<Article>();           
            try
            {
                foreach (var item in articleProvider.GetArticlesPreView())
                {
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink,                       
                        AutherId=item.AuthorId,
                        AutherName=item.AuthorName, 
                        Picture=item.Picture
                    });
                }                 
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }          
            return View("Index",articleList.ToArray());
        }
       

        [HttpGet]
        public ActionResult GetArticlesByHeadingId(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var articleList = new List<Article>();
            try
            {
                var articlesDTO = articleProvider.GetArticlesPreViewByHeadingId(id);
                if (articlesDTO != null)
                {
                    foreach (var item in articlesDTO)
                    {
                        articleList.Add(new Article()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PictureLink = item.PictureLink,
                            AutherId = item.AuthorId,
                            AutherName = item.AuthorName,
                            Picture=item.Picture

                        });
                    }
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ConnectionError = ex.Message;
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            } 
            return View("Index",articleList.ToArray());            
        }

        public ActionResult ShowUsercapabilities()
        {
            return PartialView("Usercapabilities");
        }

        [Json]
        [HttpGet]
        public JsonResult GetHeadings()
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            var headingList = new List<Heading>();
            try
            {
                foreach (var item in headingProvider.GetHeadings())
                {
                    headingList.Add(new Heading()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description
                        });
                }
                return Json(headingList, JsonRequestBehavior.AllowGet);
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return null;
            }            
        }
        [HttpGet]
        public ActionResult GetFullArticle(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var article = new Article();
            var headings = new List<Heading>();
            var articleLinks = new List<ArticleLink>();
            
            try
            {
                var languageListDTO = languageProvider.GetLanguages();
                if (languageListDTO == null)
                {
                    ViewBag.ErrorMessage = "Sorry something wrong with database...";
                    return View("ErrorView");
                }
                var languageList = new List<Language>();
                foreach (var item in languageListDTO)
                {
                    languageList.Add(new Language()
                    {
                        LanguageId = item.LanguageId,
                        LanguageName = item.LanguageName
                    });
                }
                var languageSelectList = new SelectList(languageList, "LanguageId", "LanguageName");
                ViewBag.LanguageList = languageSelectList;         
                var articleDTO = articleProvider.GetFullArticleById(id);
                if(articleDTO==null || articleDTO.Headings==null)
                {
                    ViewBag.ErrorMessage = "NotFound";
                    return View("ErrorView");
                }                
                foreach(var item in articleDTO.Headings)
                {                   
                    headings.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
                foreach(var item in articleDTO.ArticleLinks)
                {
                    articleLinks.Add(new ArticleLink() 
                    {
                        Id=item.Id,
                        Name=item.Name
                    });
                }
                article.Id = articleDTO.Id;
                article.Name = articleDTO.Name;
                article.PictureLink = articleDTO.PictureLink;
                article.Picture = articleDTO.Picture;
                article.AutherId = articleDTO.AuthorId;
                article.AutherName = articleDTO.AuthorName;
                article.Details = new Info()
                {
                    Id = articleDTO.Details.Id,
                    Date = articleDTO.Details.Date,
                    Text = articleDTO.Details.Text,
                    Language = new Language()
                    {
                        LanguageId=articleDTO.Details.Language.LanguageId,
                        LanguageName=articleDTO.Details.Language.LanguageName
                    },
                    VideoLink = articleDTO.Details.VideoLink
                };
                article.Headings = headings.ToArray();
                article.ArticleLinks = articleLinks.ToArray();               
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            } 

            return View("Article",article);
        }        

        [HttpGet]
        public ActionResult Search(string id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            var articleList = new List<Article>();
            try
            {
                var articles = articleProvider.GetArticlesPreViewByName(id);
                if(articles==null)
                {
                    return View("Index", articleList.ToArray());
                }
                foreach (var item in articles)
                {  
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink, 
                        AutherId = item.AuthorId,
                        AutherName = item.AuthorName,
                        Picture=item.Picture
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            return View("Index", articleList.ToArray());
        }
     
        [HttpGet]
        public ActionResult AdvancedSearch()
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            try
            {
                var headingsDTO = headingProvider.GetHeadings();
                if (headingsDTO == null)
                {
                    ViewBag.ErrorMessage = "There are no headings";
                    return View("ErrorView");
                }
                var headings = new List<Heading>();
                foreach (var item in headingsDTO)
                {
                    headings.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                var headingList = new SelectList(headings, "Id", "Name");
                ViewBag.HeadingList = headingList;
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            
            return View("AdvancedSearch");
        }

        [HttpPost]
        public ActionResult AdvancedSearch(AdvancedSearchModel model)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!model.HeadingId.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }            
            DateTime dateFrom = model.SearchFrom;
            DateTime dateTo = model.SearchTo;
            if(dateFrom.Year==1)
            {
                dateFrom = new DateTime(2000, 1, 1);
            }
            if(dateTo.Year==1)
            {
                dateTo = DateTime.Now;
            }            
            var articleList = new List<Article>();
            try
            {
                ArticleDTO[] articlesDTO = articleProvider.GetArticlesPreViewByHeadingIdAndDatePeriod(dateFrom, dateTo, model.HeadingId);
                if (articlesDTO != null)
                {
                    foreach (var item in articlesDTO)
                    {
                        articleList.Add(new Article()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PictureLink = item.PictureLink,
                            AutherId = item.AuthorId,
                            AutherName = item.AuthorName,
                            Picture=item.Picture
                        });
                    }
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ConnectionError = ex.Message;
                return View("ErrorView");
            }
            return View("Index", articleList.ToArray());   
        }
    }
}
