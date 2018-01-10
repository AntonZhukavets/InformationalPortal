using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.business.Interfaces;
using InformationalPortal.Models;
using InformationalPortal.business.Implementations;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.common.Logs;
using Newtonsoft.Json;
using StructureMap;

namespace InformationalPortal.Controllers
{
    public class UserController : Controller
    {
        private IUserProvider userProvider;
        private IArticleProvider articleProvider;
        private ILanguageProvider languageProvider;
        private ILoger loger;
        private IOperationResult operationResult;   
        private static byte[] picture = null;
        private const string cookieName = "AUTH_COOKIE";
        private const string errorMessage = "Parametr is invalid";
        private const string notFound = "Not found";
        private const string pictureAttrinutes = "<img src=\"data:image;base64,{0}\" width=\"200\" height=\"200\">";
         
        
        public UserController(IUserProvider userProvider, IArticleProvider articleProvider,
            ILanguageProvider languageProvider, ILoger loger, IOperationResult operationresult)
        {
            this.userProvider = userProvider;
            this.articleProvider = articleProvider;
            this.languageProvider = languageProvider;
            this.loger = loger;
            this.operationResult = operationresult;
        }        
        [HttpGet]       
        public ActionResult EditProfile(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if(!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            if(id!= Convert.ToInt32(HttpContext.User.Identity.Name) && !HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
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
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ConnectionError = ex.Message;
                return View("ErrorView");
            }           
        }

        [Json]
        [HttpPost]
        public string EditProfile(FormCollection form)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            try
            {
                bool resultOfOperation=userProvider.UpdateUser(new UserDTO()
                {
                    Id = Convert.ToInt32(form["Id"]),
                    FirstName = form["FirstName"],
                    LastName = form["LastName"],
                    Email = form["Email"],
                    Login = form["Login"],
                    Password = form["Password"]                                      
                });
                if (resultOfOperation)
                {
                    loger.Info(string.Format("Succesfully update of user with id {0}", form["Id"]));              
                    return "User profile was successfully updated";
                }
                else
                {
                    return "User profile was not updated";
                }
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            } 
            
        }
        [HttpPost]
        public int CheckPassword(FormCollection form)
        {   
            int result=0;
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            try
            {
                if (userProvider.IsValidUser(form["Login"], form["Password"]))
                {
                    result = 1;
                }
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
            }
            return result;
        }
        [Json]
        [HttpGet]
        public string RemoveProfile(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            const string successMessage = "Profile successfully removed";
            const string unSuccessMessage = "Profile not removed. Details: ";
            if (!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                return errorMessage;
            }
            try
            {
                if (userProvider.DeleteUser(id))
                {
                    loger.Info(string.Format("Succesfully removed user with id {0}", id)); 
                    return successMessage;
                }
                else
                {
                    return unSuccessMessage;
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            }            
        }       

        [Json]
        [HttpGet]
        public string ResumeProfile(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            const string successMessage = "Profile successfull resumed";
            const string unSuccessMessage = "Profile has not resumed. Details: ";
            if(!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                return errorMessage;
            }
            try
            {
                if (userProvider.ResumeUser(id))
                {
                    loger.Info(string.Format("Succesfully resumed user with id {0}", id)); 
                    return successMessage;
                }
                else
                {
                    return unSuccessMessage;
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                return ex.Message;
            }
        }
        [HttpGet]
        public ActionResult GetArticlesByUserId(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            if (id != Convert.ToInt32(HttpContext.User.Identity.Name) && !HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
                return RedirectToAction("Denied");
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
                            AutherName = item.AuthorName,
                            Picture=item.Picture
                        });
                    }
                }
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            return View("MyArticles", articleList.ToArray());
        }
        [HttpGet]
        public ActionResult AddArticle()
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            return View("AddArticle");
        }
        
        [Json]
        [HttpPost]
        public JsonResult AddArticle(FormCollection form, int[] Headings)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;           
            operationResult.ResultOfOperation = false;
            if (string.IsNullOrEmpty(form["Name"]))
            {
                operationResult.Message = "Name of article is empty";
                return Json(operationResult);
            }            
            if (string.IsNullOrEmpty(form["Text"]))
            {
                operationResult.Message = "Text of article is empty";
                return Json(operationResult);
            }
            if (string.IsNullOrEmpty(form["LanguageId"]))
            {
                operationResult.Message = "Language of article is not selected";
                return Json(operationResult);
            }
            if(Headings==null)
            {
                operationResult.Message = "Headings not selected";
                return Json(operationResult);
            }
            if (form["PictureLink"] == "unSelected" && picture!=null)
            {
                picture = null;
                operationResult.Message = "Picture not selected";
                return Json(operationResult);
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
                    Headings = headings.ToArray(),
                    Picture=picture
                });
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                operationResult.Message = ex.Message;
                return Json(operationResult);
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                operationResult.Message = ex.Message;
                return Json(operationResult);
            }
            if(resultOfAdding)
            {
                loger.Info(string.Format("User {0} succesfully added article with id {1}", HttpContext.User.ToString(), form["Id"]));
                operationResult.Message = "Article successfully added";
                operationResult.ResultOfOperation = true;
                operationResult.Redirect = Url.Action("GetArticlesByUserId", "User") + "/" + Convert.ToInt32(HttpContext.User.Identity.Name);
                return Json(operationResult);
            }
            else
            {
                operationResult.Message = "Article not added";
                return Json(operationResult);
            }     
        }

        [Json]
        [HttpPost]
        public JsonResult EditArticle(FormCollection form, int[] Headings)
        {            
            operationResult.ResultOfOperation = false;
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (string.IsNullOrEmpty(form["Name"]))
            {
                operationResult.Message = "Name of article is empty";
                return Json(operationResult);
            }            
            if (string.IsNullOrEmpty(form["Text"]))
            {
                operationResult.Message = "Text of article is empty";
                return Json(operationResult);
            }
            if (string.IsNullOrEmpty(form["LanguageId"]))
            {
                operationResult.Message = "Language of article is not selected";
                return Json(operationResult);
            }
            if (Headings == null)
            {
                operationResult.Message = "Headings not selected";
                return Json(operationResult);
            }
            if (form["PictureLink"] == "unSelected")
            {
                picture = null;                
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
                    Headings = headings.ToArray(),
                    Picture = picture
                });
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                operationResult.Message = ex.Message;
                return Json(operationResult);
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                operationResult.Message = ex.Message;
                return Json(operationResult);
            }
            if (resultOfUpdating)
            {
                loger.Info(string.Format("User {0} succesfully uodated article with id {1}", HttpContext.User.ToString(), form["Id"]));
                operationResult.Message = "Article successfully updated";
                operationResult.ResultOfOperation = true;
                return Json(operationResult);               
            }
            else
            {
                operationResult.Message = "Article not updated";
                return Json(operationResult); 
            }
        }
        
        [Json]
        [HttpPost]
        public JsonResult DeleteArticle(int? id)
        {
            var currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;            
            operationResult.ResultOfOperation = false;
            if (!id.HasValue)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));                
                operationResult.Message = errorMessage;
                return Json(operationResult);
            }
            bool resultOfDeleting = false;
            try
            {
                resultOfDeleting = articleProvider.DeleteArticle(id);
               
            }
            catch (DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));                
                operationResult.Message = errorMessage;
                return Json(operationResult);
            }
            catch (ArgumentException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));                
                operationResult.Message = errorMessage;
                return Json(operationResult);
            }
            if(resultOfDeleting)
            {
                loger.Info(string.Format("User {0} succesfully deleted article with id {1}", HttpContext.User.ToString(), id));
                operationResult.ResultOfOperation = true;
                operationResult.Message = "Article succesfully deleted";
                operationResult.Redirect = Url.Action("GetArticlesByUserId", "User") + "/" + Convert.ToInt32(HttpContext.User.Identity.Name);
                return Json(operationResult);
            }
            else 
            {
                operationResult.Message = "Article not deleted";
                return Json(operationResult);
            }
        } 

        [HttpGet]
        public ActionResult Denied()
        {
            return View("AccessDenied");
        }

        [Json]
        [HttpPost]
        public string  UploadPicture()
        {              
            foreach (var file in Request.Files)
            {
                var uploadFile = Request.Files[file.ToString()];
                if (uploadFile != null)
                {
                    var fileName = System.IO.Path.GetFileName(uploadFile.FileName);
                    picture = new byte[uploadFile.ContentLength];
                    uploadFile.InputStream.Read(picture, 0, uploadFile.ContentLength);
                }
            }
            return string.Format(pictureAttrinutes, Convert.ToBase64String(picture));
        }
    }
}
