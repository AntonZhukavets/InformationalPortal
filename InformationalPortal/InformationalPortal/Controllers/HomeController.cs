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
        public ActionResult Index()
        {
            List<Article> articleList = new List<Article>();
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
            List<string> headingsName = new List<string>();
            foreach(var item in dataProvider.GetHeadings())
            {
                headingsName.Add(item.Name);
            }
            ViewBag.HeadingsName = headingsName;
            return View(articleList);
        }
        [HttpGet]
        public ActionResult GetArticlePreView(int? id)
        {
            Article articlePreView = new Article();
            ArticleDTO aricleDTO = dataProvider.GetArticle(id);
            articlePreView.Id = aricleDTO.Id;
            articlePreView.Name = aricleDTO.Name;
            articlePreView.PictureLink = aricleDTO.PictureLink;
            return PartialView("ArticlePreView",articlePreView);
        }
        [HttpGet]
        public ActionResult GetArticle(int? id)
        {
            Article article = new Article();
            ArticleDTO aricleDTO = dataProvider.GetArticle(id);
            article.Id = aricleDTO.Id;
            article.Name = aricleDTO.Name;
            article.PictureLink = aricleDTO.PictureLink;
            article.Details = new Info() 
            {
                Id = article.Id,
                Text = aricleDTO.Details.Text,
                Language=aricleDTO.Details.Language,
                Date=aricleDTO.Details.Date,
                VideoLink=aricleDTO.Details.VideoLink
            };

            return PartialView("Article", article);

        }

    }
}
