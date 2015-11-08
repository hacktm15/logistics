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
        public new Role[] Roles;

        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
            if (Roles == null)
            {
                return true;
            }
            if (httpActionContext.Request.Headers.Contains("Authorization"))
            {
                var tr = new TokenRepository();
                Role[] myRoles;
                if (tr.IsTokenValid(httpActionContext.Request.Headers.GetValues("Authorization").First(),
                    out myRoles))
                {
                    if (Roles.Contains(Role.Self))
                    {
                        return true;
                    }
                    if (Roles.Any(x=>myRoles.Contains(x)))
                    {
                        return true;
                    }
                }
            }
            return false; //base.IsAuthorized(httpActionContext);
        }
    }
}