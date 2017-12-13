using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InfPortal.business.Interfaces;
using InformationalPortal.Models;
using InfPortal.business.Implementations;
using InfPortal.common.Exceptions;
using InfPortal.business.DTO;
using InformationalPortal.business.Implementations;

namespace InformationalPortal.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly IDataProvider dataProvider;
        const string errorMessage = "Parametr is invalid";
        public HomeController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var articleList = new List<Article>();           
            try
            {
                foreach (var item in dataProvider.GetArticles())
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
                            Date = item.Details.Date,
                            Language = item.Details.Language,
                            VideoLink = item.Details.VideoLink
                        },
                        AutherId=item.AuthorId,
                        AutherName=item.AuthorName                        
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;
                return View("ErrorView");
            }          
            return View("Index",articleList);
        }


        //[HttpGet]
        //public ActionResult GetArticlePreView(int? id)
        //{
        //    if(id==null)
        //    {
        //        ViewBag.ConnectionError = errorMessage;
        //        return View("ErrorView");
        //    }
        //    var articlePreView = new Article();
        //    try
        //    {
        //        var aricleDTO = dataProvider.GetArticle(id);
        //        articlePreView.Id = aricleDTO.Id;
        //        articlePreView.Name = aricleDTO.Name;
        //        articlePreView.PictureLink = aricleDTO.PictureLink;
        //        articlePreView.AutherId = aricleDTO.AuthorId;
        //        articlePreView.AutherName = aricleDTO.AuthorName;
        //    }
        //    catch (DataBaseConnectionException ex)
        //    {
        //        ViewBag.ConnectionError = ex.Message;
        //    }

        //    return PartialView("ArticlePreView",articlePreView);
        //}
        [HttpGet]
        public ActionResult GetArticle(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var article = new Article();            
            try
            {
                var aricleDTO = dataProvider.GetArticle(id);
                article.Id = aricleDTO.Id;
                article.Name = aricleDTO.Name;
                article.PictureLink = aricleDTO.PictureLink;
                article.Details = new Info()
                {
                    Id = article.Id,
                    Text = aricleDTO.Details.Text,
                    Language = aricleDTO.Details.Language,
                    Date = aricleDTO.Details.Date,
                    VideoLink = aricleDTO.Details.VideoLink
                };
                article.AutherId = aricleDTO.AuthorId;
                article.AutherName = aricleDTO.AuthorName;
            }
            catch(DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;                
            }

            return PartialView("Article", article);
        }

        [HttpGet]
        public ActionResult GetArticlesByHeadingId(int? id)
        {            
            if (id == null)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var articleList = new List<Article>();
            try
            {
                foreach (var item in dataProvider.GetArticlesByHeadingId(id))
                {
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink ,
                        AutherId=item.AuthorId,
                        AutherName=item.AuthorName
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;
            }           
            return View("Index",articleList);            
        }
        public ActionResult ShowUsercapabilities()
        {

            return PartialView("Usercapabilities");
        }
        [Json]
        [HttpGet]
        public JsonResult GetHeadings()
        {
            var headingList = new List<Heading>();
            try
            {
                foreach (var item in dataProvider.GetHeadings())
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
            catch(DataBaseConnectionException)
            {
                return null;
            }
            
        }
        [HttpGet]
        public ActionResult GetFullArticle(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var article = new Article();
            try
            {
                var aricleDTO = dataProvider.GetArticle(id);
                if (aricleDTO.Id != 0)
                {
                    article.Id = aricleDTO.Id;
                    article.Name = aricleDTO.Name;
                    article.PictureLink = aricleDTO.PictureLink;
                    article.AutherId = aricleDTO.AuthorId;
                    article.AutherName = aricleDTO.AuthorName;
                    article.Details = new Info()
                    {
                        Id = aricleDTO.Details.Id,
                        Date = aricleDTO.Details.Date,
                        Text = aricleDTO.Details.Text,
                        Language = aricleDTO.Details.Language,
                        VideoLink = aricleDTO.Details.VideoLink
                    };
                }
                else
                {
                    ViewBag.ErrorMessage = "NotFound";
                    return View("ErrorView");
                }
            }
            catch(DataBaseConnectionException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
           

            return View("Article",article);
        }
        [HttpGet]
        public string[] GetArticleNamesByInput(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var names = dataProvider.GetArticleNamesByInput(name);
                    return names;
                }
                catch (DataBaseConnectionException ex)
                {
                                        
                }
            }
            return null;           
        }

        [HttpGet]
        public ActionResult Search(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var articleList = new List<Article>();
                try
                {
                    foreach (var item in dataProvider.GetArticlesByName(id))
                    {
                        articleList.Add(new Article()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PictureLink = item.PictureLink,                            
                            AutherId = item.AuthorId,
                            AutherName = item.AuthorName
                        });
                    }
                }
                catch (DataBaseConnectionException ex)
                {
                    ViewBag.ConnectionError = ex.Message;
                    return View("ErrorView");
                }
                return View("Index", articleList);

            }
            return View("Test");
        }

    }
}
