using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;

namespace LogisticsAPI.Authorization
{
    public class LDAPAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
           
            UserRights userRights;
            if (httpActionContext.Request.Headers.Contains("Authorization"))
            {
                if (TokenRepository.IsTokenValid(httpActionContext.Request.Headers.GetValues("Authorization").First(),
                    out userRights))
                {
                    return true;
                }
            }

            return false;//base.IsAuthorized(httpActionContext);
        }
    }
}