using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfPortal.business.Interfaces;
using InformationalPortal.Models;
using InfPortal.business.Implementations;
using InfPortal.common.Exceptions;
using InfPortal.business.DTO;

namespace InformationalPortal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        readonly IDataProvider dataProvider;
        public HomeController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Article> articleList = new List<Article>();
            List<Heading> headings = new List<Heading>();
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
                        }
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;
            }
           
            foreach(var item in dataProvider.GetHeadings())
            {
                headings.Add(new Heading() { 
                    Id=item.Id,
                    Name=item.Name,
                    Description=item.Description
                });
            }
            ViewBag.Headings = headings;
            return View(articleList);
        }


        [HttpGet]
        public ActionResult GetArticlePreView(int? id)
        {
            Article articlePreView = new Article();
            try
            {
                ArticleDTO aricleDTO = dataProvider.GetArticle(id);
                articlePreView.Id = aricleDTO.Id;
                articlePreView.Name = aricleDTO.Name;
                articlePreView.PictureLink = aricleDTO.PictureLink;
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;
            }

            return PartialView("ArticlePreView",articlePreView);
        }
        [HttpGet]
        public ActionResult GetArticle(int? id)
        {
            Article article = new Article();
            try
            {
                ArticleDTO aricleDTO = dataProvider.GetArticle(id);
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
            List<Article> articleList = new List<Article>();
            try
            {
                foreach (var item in dataProvider.GetArticlesByHeadingId(id))
                {
                    articleList.Add(new Article()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PictureLink = item.PictureLink                       
                    });
                }
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ConnectionError = ex.Message;
            }           
            return View("ArticlePreView",articleList);            
        }

    }
}
