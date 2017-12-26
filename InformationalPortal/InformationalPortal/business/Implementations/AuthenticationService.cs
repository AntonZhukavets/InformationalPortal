using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using InformationalPortal.business.Enums;
using InformationalPortal.business.Interfaces;
using InformationalPortal.Models;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.business.Providers;
using InfPortal.common.Exceptions;
using Newtonsoft.Json;


namespace InformationalPortal.business.Implementations
{
    public class AuthenticationService: IAuthenticationService
    {
        IUserProvider userProvider;
        const string cookieName = "AUTH_COOKIE";
        private int userId=0; 
        public int UserId
        {
            get { return userId; }
        }        
        public AuthenticationService(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }
        public AuthenticationResult Login(string login, string password)
        {

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return AuthenticationResult.EmptyCredentials;
            }
            try
            {
                var userDTO = userProvider.GetUserByLoginAndPassword(login, password);
                if (userDTO != null)
                {
                    userId = userDTO.Id;
                    var user = new User();
                    user.Id = userDTO.Id;
                    user.Login = userDTO.Login;
                    user.Email = userDTO.Email;
                    user.RoleId = userDTO.role.Id;
                    user.RoleName = userDTO.role.Name;
                    string userData = JsonConvert.SerializeObject(user);
                    var ticket = new FormsAuthenticationTicket(2, user.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(cookieName, encryptedTicket);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return AuthenticationResult.NoErrors;
                }             
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return AuthenticationResult.InValidCredentials;
        }
        public void Logout()
        {
            var cookie = HttpContext.Current.Response.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Value = string.Empty;
            }
        }
    }
}