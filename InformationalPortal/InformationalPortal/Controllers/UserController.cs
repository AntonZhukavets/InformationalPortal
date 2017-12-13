using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InfPortal.business.Interfaces;
using InfPortal.business.DTO;
using InformationalPortal.Models;
using InfPortal.common.Exceptions;
using InformationalPortal.business.Implementations;
using System.Web.Security;
using Newtonsoft.Json;

namespace InformationalPortal.Controllers
{
    public class UserController : Controller
    {
        private IUserProvider userProvider;
        private IDataProvider dataProvider;
        const string cookieName = "AUTH_COOKIE";
        const string errorMessage = "Parametr is invalid";
        public UserController(IUserProvider userProvider, IDataProvider dataProvider)
        {
            this.userProvider = userProvider;
            this.dataProvider = dataProvider;
        }

        
        [HttpGet]       
        public ActionResult EditProfile(int? id)
        {
            if(id==null)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }

            if(id!= Convert.ToInt32(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Denied");
            }
            try
            {
                var  userDTO=userProvider.GetUserById(id);
                var editableUser = new RegisterModel();
                editableUser.FirstName = userDTO.FirstName;                
                editableUser.LastName = userDTO.LastName;
                editableUser.Login = userDTO.Login;
                editableUser.Email = userDTO.Email;
                editableUser.Password = userDTO.Password;
                editableUser.Age = userDTO.Age;

                return View("EditProfile", editableUser);
            }
            catch(DataBaseConnectionException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }           
        }

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
                    Password = form["Password"],
                    Age = Convert.ToInt32(form["Age"])                    
                });
                return "User profile was successfully updated";
            }
            catch(DataBaseConnectionException ex)
            {
                return ex.Message;
            }
            
        }
        [HttpPost]
        public int CheckPassword(FormCollection form)
        {   
            int id=0;
            if(userProvider.IsValidUser(form["Login"], form["Password"]))
            {
                id = userProvider.GetUserByLogin(form["Login"]).Id;
            }
            return id;
        }

        [HttpGet]
        public string RemoveProfile(int id)
        {
            const string successMessage = "Profile successfull removed";
            const string errorMessage = "Profile has not removed. Details: ";
            return successMessage;
        }
        [HttpGet]
        public ActionResult GetArticlesByUserId(int? id)
        {
            if (id != Convert.ToInt32(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Denied");
            }
            if(id==null)
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
            var articleList = new List<Article>();            
            try
            {
                foreach (var item in dataProvider.GetArticlesByUserId(id))
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
            }
            return View("MyArticles", articleList);
        }
        [HttpGet]
        public ActionResult AddArticle()
        {
            return View("AddArticle");
        }
        [HttpGet]
        public ActionResult Denied()
        {
            return View("AccessDenied");
        }



    }
}
