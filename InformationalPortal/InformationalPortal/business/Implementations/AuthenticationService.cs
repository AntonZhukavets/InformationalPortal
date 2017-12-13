using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InformationalPortal.business.Interfaces;
using InformationalPortal.Models;
using InformationalPortal.business.Enums;
using InfPortal.business.Implementations;
using InfPortal.business.DTO;
using Newtonsoft.Json;
using System.Web.Security;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;

namespace InformationalPortal.business.Implementations
{
    public class AuthenticationService: IAuthenticationService
    {
        UserProvider userProvider;
        const string cookieName = "AUTH_COOKIE";
        
        public AuthenticationService()
        {
            userProvider = new UserProvider();
        }

        public AuthenticationResult Login(string login, string password)
        {

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return AuthenticationResult.EmptyCredentials;
            }
            try
            {
                if (userProvider.IsValidUser(login, password))
                {
                    UserDTO userDTO = userProvider.GetUserByLogin(login);
                    SimpleUserModel user = new SimpleUserModel();
                    user.Id = userDTO.Id;
                    user.Login = userDTO.Login;
                    user.Email = userDTO.Email;
                    string userData = JsonConvert.SerializeObject(user);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, user.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(cookieName, encryptedTicket);
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
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Value = string.Empty;
            }
        }
    }
}