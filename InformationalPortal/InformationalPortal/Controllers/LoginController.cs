using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.Models;
using InfPortal.business;
using InfPortal.business.Interfaces;
using InfPortal.business.Implementations;
using InfPortal.business.DTO;
using InformationalPortal.business.Enums;
using InformationalPortal.business.Implementations;
using InfPortal.common.Exceptions;
namespace InformationalPortal.Controllers
{
    public class LoginController : Controller
    {
        private IUserProvider userProvider;
        public LoginController(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View("Registration");
        }
        [HttpPost]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userProvider.Registration(new UserDTO()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Login = model.Login,
                        Password = model.Password,
                        Age = model.Age
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("ErrorView");
                }
                return RedirectToAction("Index", "Home");
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
            
                AuthenticationService authService = new AuthenticationService();
                try
                {
                    AuthenticationResult authResult = authService.Login(model.Login, model.Password);
                    if (authResult == AuthenticationResult.NoErrors)
                    {
                        return RedirectToAction("Index", "Home");
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
            
        }
        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationService authService = new AuthenticationService();
            authService.Logout();
            return RedirectToAction("Index", "Home");
        }


    }
}
