using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.business.Enums;
using InformationalPortal.business.Implementations;
using InformationalPortal.business.Interfaces;
using InformationalPortal.Models;
using InfPortal.business;
using InfPortal.business.DTO;
using InfPortal.business.Providers;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;


namespace InformationalPortal.Controllers
{
    public class LoginController : Controller
    {
        private IUserProvider userProvider;
        private IAuthenticationService authService;
        public LoginController(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
            this.authService = new AuthenticationService(userProvider);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View("Registration");
        }
        [HttpPost]
        public ActionResult Registration(RegisterModel model)
        {
            bool resultOfResgistration = false;
            ViewBag.RegistrationMessage = "Registration is not successfull";
            if (ModelState.IsValid)
            {
                try
                {
                    resultOfResgistration=userProvider.Registration(new UserDTO()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Login = model.Login,
                        Password = model.Password                        
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("ErrorView");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.ConnectionError = ex.Message;
                    return View("ErrorView");
                } 
                if(resultOfResgistration)
                {
                    ViewBag.RegistrationMessage = "Succesfull resgistration";
                }           
            }
            return View("Registration", model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                AuthenticationResult authResult = authService.Login(model.Login, model.Password);
                if (authResult == AuthenticationResult.NoErrors)
                {
                    return RedirectToAction("GetArticlesByUserId", "User", new { id = authService.UserId }); 
                }
                if (authResult == AuthenticationResult.EmptyCredentials)
                {
                        return View("Login", model);
                }
                ViewBag.message = "Invalid credentials!";
                return View("Login", model);
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
        [HttpGet]
        public ActionResult Logout()
        {           
            authService.Logout();
            return RedirectToAction("Index", "Home");
        }


    }
}
