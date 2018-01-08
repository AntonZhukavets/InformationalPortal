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
using InfPortal.common.Logs;


namespace InformationalPortal.Controllers
{
    public class LoginController : Controller
    {
        private ILoger loger;
        private IUserProvider userProvider;
        private IAuthenticationService authService;
        public LoginController(IUserProvider userProvider, ILoger loger)
        {
            this.userProvider = userProvider;
            this.loger = loger;
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
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
                if(resultOfResgistration)
                {
                    loger.Info(string.Format("Succesfully registration for user with login {0}", model.Login));
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
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            try
            {
                AuthenticationResult authResult = authService.Login(model.Login, model.Password);
                if (authResult == AuthenticationResult.NoErrors)
                {
                    loger.Info(string.Format("Succesfully login for user with login {0}", model.Login));
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
        [HttpGet]
        public ActionResult Logout()
        {
            string currentUser = HttpContext.User.ToString();
            authService.Logout();
            loger.Info(string.Format("Succesfully logout for user with login {0}", currentUser));
            return RedirectToAction("Index", "Home");
        }
    }
}
