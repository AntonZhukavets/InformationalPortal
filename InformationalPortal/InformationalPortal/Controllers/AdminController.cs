using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using InformationalPortal.business.Implementations;
using InformationalPortal.Models;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;


namespace InformationalPortal.Controllers
{
    public class AdminController : Controller
    {
        private IHeadingProvider headingProvider;
        private IUserProvider userProvider;
        public AdminController(IHeadingProvider headingProvider, IUserProvider userProvider)
        {
            this.headingProvider = headingProvider;
            this.userProvider = userProvider;
        }
        [HttpGet]
        public ActionResult AdminsArea()
        {
            if(!HttpContext.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Denied", "User");
            }
            return View("AdminsArea");
        }
        [HttpGet]
        public ActionResult WorkWithHeadings()
        {
            if (!HttpContext.User.IsInRole("Administrator"))
            {
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
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }    
        }
        [HttpGet]
        public ActionResult AddHeading()
        {
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Denied", "User");
            }
            return View();
        }
        [Json]
        [HttpPost]
        public string AddHeading(FormCollection form)
        {
            bool resultOfAdding = false;
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
                    return "Heading successfully added";
                }
                return "Heading not added";
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
        [Json]
        [HttpPost]
        public string EditHeading(FormCollection form)
        {
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
                return "Heading successfully updated";
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
        [Json]
        [HttpGet]
        public string DeleteHeading(int? id)
        {
            if (id == null)
            {
                return "Heading not selected";
            }
            bool resultOfEdition = false;
            try
            {
                resultOfEdition = headingProvider.DeleteHeading(id);                
                return "Heading successfully deleted";
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
        public ActionResult WorkWithUsers()
        {
            const string noUsersError = "There are no any users. Add some.";
            if (!HttpContext.User.IsInRole("Administrator"))
            {
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
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorView");
            }
            
        }

        [Json]
        [HttpGet]
        public string MakeAdmin(int? id)
        {
            const string successMessage = "Profile successfully upgraded";
            const string unSuccessMessage = "Profile has not upgraded. Details: ";
            const string notSelectedMessage = "User has not selected";
            if (id.HasValue)
            {
                try
                {
                    if (userProvider.MakeAdmin(id))
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
            return notSelectedMessage;
        }

    }
}
