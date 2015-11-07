using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LogisticsAPI.Authorization
{
    public class LDAPAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            //logger.Info("User name IsAuthenticated " + httpContext.User.Identity.IsAuthenticated);
            //logger.Info("User name " + httpContext.User.Identity.Name);
            if (httpContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                // return true;
            }
            return base.IsAuthorized(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Error/Unauthorized");
        }
    }
}