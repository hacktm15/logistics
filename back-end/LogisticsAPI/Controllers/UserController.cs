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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LogisticsAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [HttpOptions]
        [LDAPAuthorize(Roles = new[] {Role.Admin, Role.Self, Role.Self})]
        public object GetInfo(string cnUser)
        {
            if (Request.Method == HttpMethod.Options)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            try
            {
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                var userInfo = ur.GetUserInfo(origAuthRequst.User, origAuthRequst.Password, cnUser,
                    new[] {"displayName", "telephoneNumber", "title", "mail"});
                return Request.CreateResponse(userInfo.Count > 1 ? HttpStatusCode.OK : HttpStatusCode.NotFound, userInfo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [HttpOptions]
        [LDAPAuthorize(Roles = new[] {Role.Self})]
        public object UpdateMyInfo()
        {
            if (Request.Method == HttpMethod.Options)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            try
            {
                var jsonString = Request.Content.ReadAsStringAsync().Result;
                var propertiesToUpdate = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                if (propertiesToUpdate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                if (propertiesToUpdate.ContainsKey("userPassword"))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Use ChangeMyPassword api to change your password!");
                }
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                ur.ChangeUserInfo(origAuthRequst.User, origAuthRequst.Password, propertiesToUpdate);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [HttpOptions]
        [LDAPAuthorize(Roles = new[] { Role.Self })]
        public object ChangeMyPassword()
        {
            if (Request.Method == HttpMethod.Options)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            var jsonString = Request.Content.ReadAsStringAsync().Result;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            if (dict == null || !dict.ContainsKey("newPassword"))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var newPassword = dict["newPassword"];
            try
            {
                var tr = new TokenRepository();
                var ur = new UserRepository();
                var origAuthRequst = tr.GetAuthRequestFromToken(Request.Headers.GetValues("Authorization").First());
                if (ur.ChangeUserInfo(origAuthRequst.User, origAuthRequst.Password,
                    new Dictionary<string, string> {{"userPassword", newPassword}}))
                {
                    tr.InvalidateTokensForUser(origAuthRequst.User,
                        Request.Headers.GetValues("Authorization").FirstOrDefault());
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
