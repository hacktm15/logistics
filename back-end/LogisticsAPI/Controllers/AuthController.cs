using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;

namespace LogisticsAPI.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetToken(AuthRequest model)
        {
            try
            {
                var ur = new UserRepository();
                Role[] roles;
                if (string.IsNullOrEmpty(model.User) || string.IsNullOrEmpty(model.Password) ||
                    !ur.CheckUserCredentials(model.User, model.Password, out roles))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user or password!");
                }
                else
                {
                    var tr = new TokenRepository();
                    var tokenModel = tr.GenerateAndRegisterTokenForUserWithRights(model.User,
                        model.Password, roles);
                    var tokenReponse = new TokenResponse()
                    {
                        UserRights = string.Join(",", roles),
                        ExpirationDateTime = tokenModel.ExpirationDateTime,
                        Token = tokenModel.Token
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, tokenReponse);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}