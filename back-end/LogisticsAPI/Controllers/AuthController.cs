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
            if (string.IsNullOrEmpty(model.User) || string.IsNullOrEmpty(model.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user or password!");
            }
            if (UserRepository.CheckUserCredentials(model.User, model.Password))
            {

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user or password!");
            }
        }
    }
}