using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.Models;
using InformationalPortal.business.Implementations;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using Newtonsoft.Json;

namespace InformationalPortal.Controllers
{
    public class UserController : Controller
    {
        private IUserProvider userProvider;
        private IArticleProvider articleProvider;
        private ILanguageProvider languageProvider;
        private const string cookieName = "AUTH_COOKIE";
        private const string errorMessage = "Parametr is invalid";
        private const string notFound = "Not found";
        public UserController(IUserProvider userProvider, IArticleProvider articleProvider, ILanguageProvider languageProvider)
        {
            this.userProvider = userProvider;
            this.articleProvider = articleProvider;
            this.languageProvider = languageProvider;
        }        
        [HttpGet]       
        public ActionResult EditProfile(int? id)
        {
            if(!id.HasValue)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }

            if(id!= Convert.ToInt32(HttpContext.User.Identity.Name) && !HttpContext.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Denied");
            }
            try
            {
                var  userDTO=userProvider.GetUserById(id);
                if(userDTO==null)
                {
                    ViewBag.ErrorMessage = notFound;
                    return View("ErrorView");
                }
                var editableUser = new User();
                editableUser.Id = userDTO.Id;
                editableUser.FirstName = userDTO.FirstName;                
                editableUser.LastName = userDTO.LastName;
                editableUser.Login = userDTO.Login;
                editableUser.Email = userDTO.Email;
                editableUser.Password = userDTO.Password;
                editableUser.IsBlocked = userDTO.IsBlocked;
                return View("EditProfile", editableUser);
            }
            catch(DataBaseConnectionException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                ViewBag.ConnectionError = ex.Message;
                return View("ErrorView");
            }           
        }

        [Json]
        [HttpPost]
        public string EditProfile(FormCollection form)
        {
            try
            {
                userProvider.UpdateUser(new UserDTO()
                {
                    Id = Convert.ToInt32(form["Id"]),
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    Email = form["Email"],
                    Login = form["Login"],
                    Password = form["Password"]                                      
                });
                return "User profile was successfully updated";
            }
            catch(DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            } 
            
        }
        [HttpPost]
        public int CheckPassword(FormCollection form)
        {   
            int result=0;
            try
            {
                if (userProvider.IsValidUser(form["Login"], form["Password"]))
                {
                    result = 1;
                }
            }
            catch(DataBaseConnectionException)
            {

            }
            return result;
        }
        [Json]
        [HttpGet]
        public string RemoveProfile(int? id)
        {
            const string successMessage = "Profile successfull removed";
            const string unSuccessMessage = "Profile has not removed. Details: ";
            try
            {
                if (userProvider.DeleteUser(id))
                {
                    return successMessage;
                }
                else
                {
                    return unSuccessMessage;
                }
            }
            catch (DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            
        }       

        [Json]
        [HttpGet]
        public string ResumeProfile(int? id)
        {
            const string successMessage = "Profile successfull resumed";
            const string unSuccessMessage = "Profile has not resumed. Details: ";
            try
            {
                if (userProvider.ResumeUser(id))
                {
                    return successMessage;
                }
                else
                {
                    return unSuccessMessage;
                }
            }
            catch (DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }

        }
        [HttpGet]
        public ActionResult GetArticlesByUserId(int? id)
        {
            if (id != Convert.ToInt32(HttpContext.User.Identity.Name) && !HttpContext.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Denied");
            }
            if(!id.HasValue)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var articleList = new List<Article>();            
            try
            {
                var articleDTOList = articleProvider.GetArticlesPreViewByUserId(id);
                if (articleDTOList != null)
                {
                    foreach (var item in articleDTOList)
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
            }
            catch (DataBaseConnectionException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            return View("MyArticles", articleList);
        }
        [HttpGet]
        public ActionResult AddArticle()
        {
            try 
            {
                var languageListDTO = languageProvider.GetLanguages();
                if (languageListDTO == null)
                {
                    ViewBag.ErrorMessage = "Sorry something wrong with database...";
                    return View("ErrorView");
                }
                var languageList = new List<Language>();
                foreach(var item in languageListDTO)
                {
                    languageList.Add(new Language()
                    {
                        LanguageId = item.LanguageId,
                        LanguageName = item.LanguageName
                    });
                }
                var languageSelectList = new SelectList(languageList, "LanguageId", "LanguageName");
                ViewBag.LanguageList = languageSelectList;
            }
            catch(DataBaseConnectionException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            return View("AddArticle");
        }
        
        [Json]
        [HttpPost]
        public string AddArticle(FormCollection form, int[] Headings)
        {
            if (string.IsNullOrEmpty(form["Name"]))
            {
                return "Name of article is empty";
            }
            if (string.IsNullOrEmpty(form["PictureLink"]))
            {
                return "Picture is not added";
            }
            if (string.IsNullOrEmpty(form["Text"]))
            {
                return "Text of article is empty";
            }
            if (string.IsNullOrEmpty(form["LanguageId"]))
            {
                return "Language of article is not selected";
            }
            if(Headings==null)
            {
                return "Headings not selected";
            }            
            var headings = new List<HeadingDTO>();
            bool resultOfAdding=false;
            foreach (var item in Headings)
            {
                headings.Add(new HeadingDTO() 
                {
                    Id = Convert.ToInt32(item)
                });
            }
            try
            {
                resultOfAdding = articleProvider.AddArticle(new ArticleDTO()
                {
                    Name = form["Name"],
                    PictureLink = form["PictureLink"],
                    AuthorId = Convert.ToInt32(HttpContext.User.Identity.Name),
                    Details = new InfoDTO()
                    {
                        Text = form["Text"],
                        Date = DateTime.Now,
                        Language = new LanguageDTO()
                        {
                            LanguageId = Convert.ToInt32(form["LanguageId"])
                        }, 
                        VideoLink = form["VideoLink"]
                    },
                    Headings = headings.ToArray()
                });
            }
            catch (DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            return "Article successfully added";
           
        }


        [Json]
        [HttpPost]
        public string EditArticle(FormCollection form, int[] Headings)
        {
            if (string.IsNullOrEmpty(form["Name"]))
            {
                return "Name of article is empty";
            }
            if (string.IsNullOrEmpty(form["PictureLink"]))
            {
                return "Picture is not added";
            }
            if (string.IsNullOrEmpty(form["Text"]))
            {
                return "Text of article is empty";
            }
            if (string.IsNullOrEmpty(form["LanguageId"]))
            {
                return "Language of article is not selected";
            }
            if (Headings == null)
            {
                return "Headings not selected";
            } 
            var headings = new List<HeadingDTO>();
            bool resultOfUpdating = false;
            foreach (var item in Headings)
            {
                headings.Add(new HeadingDTO()
                {
                    Id = Convert.ToInt32(item)
                });
            }
            try
            {
                resultOfUpdating = articleProvider.EditArticle(new ArticleDTO()
                {
                    Id=Convert.ToInt32(form["Id"]),
                    Name = form["Name"],
                    PictureLink = form["PictureLink"],                   
                    Details = new InfoDTO()
                    {
                        Text = form["Text"],
                        Date = DateTime.Now,
                        Language = new LanguageDTO()
                        {
                            LanguageId = Convert.ToInt32(form["LanguageId"])
                        },
                        VideoLink = form["VideoLink"]
                    },
                    Headings = headings.ToArray()
                });
            }
            catch (DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
            return "Article successfully updated";
        }
        
        [Json]
        [HttpPost]
        public string DeleteArticle(int? id)
        {            
            if (!id.HasValue)
            {
                return errorMessage;
            }
            bool resultOfDeleting = false;
            try
            {
                resultOfDeleting = articleProvider.DeleteArticle(id);
               
            }
            catch (DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            catch (ArgumentException ex)
            {

                return ex.Message;
            }
            if(resultOfDeleting)
            {
                return "Article succesfully deleted";
            }
            else 
            {
                return "Article not deleted";
            }
        } 

        [HttpGet]
        public ActionResult Denied()
        {
            return View("AccessDenied");
        }
    }
}
