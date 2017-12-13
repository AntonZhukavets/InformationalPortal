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
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                try
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    SimpleUserModel user = JsonConvert.DeserializeObject<SimpleUserModel>(ticket.UserData);
                    UserPrincipal userPrincipal = new UserPrincipal(user.Id);
                    HttpContext.Current.User = userPrincipal;
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                HttpContext.Current.User = new UserPrincipal(0);
            }
        }
    }
}