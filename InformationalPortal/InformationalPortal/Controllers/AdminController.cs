using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.business.Implementations;
using InformationalPortal.Models;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.common.Logs;


namespace InformationalPortal.Controllers
{
    public class AdminController : Controller
    {
        private ILoger loger;
        private IHeadingProvider headingProvider;
        private IUserProvider userProvider;
        public AdminController(IHeadingProvider headingProvider, IUserProvider userProvider, ILoger loger)
        {
            this.headingProvider = headingProvider;
            this.userProvider = userProvider;
            this.loger = loger;
        }
        [HttpGet]
        public ActionResult AdminsArea()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if(!HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
                return RedirectToAction("Denied", "User");
            }
            return View("AdminsArea");
        }
        [HttpGet]
        public ActionResult WorkWithHeadings()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
                return RedirectToAction("Denied", "User");
            }
            const string noHeadingError="There are no any heading. Add some.";
            try
            {
                var headingsDTO = headingProvider.GetHeadings();
                if(headingsDTO==null)
                {
                    ViewBag.ErrorMessage = noHeadingError;
                    return View("ErrorView");
                }
                var headings = new List<Heading>();
                foreach(var item in headingsDTO)
                {
                    headings.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });               
                }
                return View(headings.ToArray());
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }    
        }
        [HttpGet]
        public ActionResult AddHeading()
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
                return RedirectToAction("Denied", "User");
            }
            return View();
        }
        [Json]
        [HttpPost]
        public string AddHeading(FormCollection form)
        {
            bool resultOfAdding = false;
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if(string.IsNullOrEmpty(form["Name"]))
            {
                return "Empty heading name";
            }
            HeadingDTO heading = new HeadingDTO()
            {
                Name = form["Name"],
                Description = form["Description"]
            };
            try
            {
                resultOfAdding = headingProvider.AddHeading(heading);
                if(resultOfAdding)
                {
                    loger.Info(string.Format("Succesfully add new heading with name {0}", heading.Name));
                    return "Heading successfully added";
                }
                return "Heading not added";
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
        [Json]
        [HttpPost]
        public string EditHeading(FormCollection form)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if(form==null)
            {
                return "Heading not selected";
            }
            bool resultOfEdition = false;
            try
            {
                resultOfEdition = headingProvider.EditHeading(new HeadingDTO()
                  {
                      Id = Convert.ToInt32(form["Id"]),
                      Name = form["Name"],
                      Description = form["Description"]
                  });
                loger.Info(string.Format("Succesfully edit of heading with id {0}, new name is {1}, new description is {2}", form["Id"], form["Name"], form["Description"]));
                return "Heading successfully updated";
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
        [Json]
        [HttpGet]
        public string DeleteHeading(int? id)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (id == null)
            {
                loger.Error(string.Format(StringsToLogger.invalidParametrToLogger, currentMethodName));
                return "Heading not selected";
            }
            bool resultOfEdition = false;
            try
            {
                resultOfEdition = headingProvider.DeleteHeading(id);
                loger.Info(string.Format("Succesfully delted heading with id {0}", id.ToString()));
                return "Heading successfully deleted";
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
        public ActionResult WorkWithUsers()
        {
            const string noUsersError = "There are no any users.";
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                loger.Warning(string.Format(StringsToLogger.accessDeniedToLogger, currentMethodName, HttpContext.User.ToString()));
                return RedirectToAction("Denied", "User");
            }
            try 
            {
                var usersDTO = userProvider.GetAllUsers();
                if(usersDTO==null)
                {
                    ViewBag.ErrorMessage = noUsersError;
                    return View("ErrorView");
                }
                var users = new List<User>();
                foreach (var item in usersDTO)
                {
                    users.Add(new User()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Login = item.Login,
                        Password = item.Password,
                        RoleName = item.role.Name,
                        RoleId=item.role.Id,
                        IsBlocked = item.IsBlocked
                    });
                }
                return View(users.ToArray());        
            }
            catch(DataBaseConnectionException ex)
            {
                loger.Error(string.Format(StringsToLogger.exceptionToLogger, currentMethodName, ex.Message));
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            
        }

        [Json]
        [HttpGet]
        public string MakeAdmin(int? id)
        {
            string currentMethodName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            const string successMessage = "Profile successfully upgraded";
            const string unSuccessMessage = "Profile has not upgraded. Details: ";
            const string notSelectedMessage = "User has not selected";
            if (id.HasValue)
            {
                try
                {
                    if (userProvider.MakeAdmin(id))
                    {
                        loger.Info(string.Format("user with id {0} is admin now", id ));
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
            return notSelectedMessage;
        }

    }
}
