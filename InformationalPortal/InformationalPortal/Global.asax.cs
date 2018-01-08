using InformationalPortal.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using InformationalPortal.Models;
using Newtonsoft.Json;
using InformationalPortal.business.Implementations;
using InfPortal.business.Interfaces;
using InfPortal.business.Providers;
using InformationalPortal.business.Interfaces;

namespace InformationalPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start() 
        {
            
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            LoggerConfig.RegistrLoger();

        }
        public void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            const string cookieName = "AUTH_COOKIE";
            IAuthenticationUser user = new AuthenticationUser();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                try
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var userFromCookie = JsonConvert.DeserializeObject<User>(ticket.UserData);
                    user.Id = userFromCookie.Id;
                    user.Login = userFromCookie.Login;
                    user.RoleName = userFromCookie.RoleName;
                    UserPrincipal userPrincipal = new UserPrincipal(user);                   
                    HttpContext.Current.User = userPrincipal;                    
                }
                catch(Exception)
                {

                }
            }
            else
            {                
                HttpContext.Current.User = new UserPrincipal(user);
            }
        }
    }
}