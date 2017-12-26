using System.Reflection;
using System.Web.Mvc;

namespace InformationalPortal.business.Implementations
{
    public class JsonAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}