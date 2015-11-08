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
        public HttpResponseMessage Post(AuthRequest model)
        {
            try
            {
                UserRights userRights;
                if (string.IsNullOrEmpty(model.User) || string.IsNullOrEmpty(model.Password) ||
                    !UserRepository.CheckUserCredentials(model.User, model.Password, out userRights))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user or password!");
                }
                else
                {
                    var tokenModel = TokenRepository.GenerateAndRegisterTokenForUserWithRights(model.User, userRights);
                    var tokenReponse = new TokenResponse()
                    {
                        UserRights = userRights.ToString(),
                        ExpirationDateTime = tokenModel.ExpirationDateTime,
                        Token = tokenModel.Token
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, tokenReponse);
                }
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}