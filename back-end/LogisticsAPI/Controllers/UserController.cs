using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LogisticsAPI.Authorization;
using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;

namespace LogisticsAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [LDAPAuthorize(Roles = new[] {Role.Admin, Role.Self, Role.Self})]
        public object GetInfo(string cnUser)
        {
            try
            {
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                var userInfo = ur.GetUserInfo(origAuthRequst.User, origAuthRequst.Password, cnUser,
                    new[] {"displayName", "telephoneNumber", "title", "mail"});
                return Request.CreateResponse(HttpStatusCode.OK, userInfo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [LDAPAuthorize(Roles = new[] {Role.Self})]
        public object UpdateMyInfo(Dictionary<string, string> propertiesToUpdate)
        {
            try
            {
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                ur.ChangeUserInfo(origAuthRequst.User, origAuthRequst.Password, propertiesToUpdate);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [LDAPAuthorize(Roles = new[] { Role.Self })]
        public object ChangeMyPassword(string newPassword)
        {
            try
            {
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                ur.ChangeUserInfo(origAuthRequst.User, origAuthRequst.Password,
                    new Dictionary<string, string> {{"userPassword", newPassword}});
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
