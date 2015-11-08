using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.Models;

namespace LogisticsAPI.DataAccess
{
    public class TokenRepository
    {
        public static TokenModel GenerateAndRegisterTokenForUserWithRights(string username, UserRights userRights)
        {
            var tokenModel = new TokenModel
            {
                UserRights = userRights,
                Username = username,
                ExpirationDateTime = DateTime.Now.AddDays(1),
                Token =
                    Convert.ToBase64String(
                        Encoding.Default.GetBytes((username + "_" + userRights + "_" + Guid.NewGuid())))
            };
            using (var db = new DBUnitOfWork())
            {
                db.Repository<TokenModel>().Add(tokenModel);
            }
            return tokenModel;
        }

        public static bool IsTokenValid(string token, out UserRights userRights)
        {
            userRights = UserRights.Anonymous;
            using (var db = new DBUnitOfWork())
            {
                var tokenModel = db.Repository<TokenModel>().Find(x => x.Token.Equals(token));
                if (tokenModel != null)
                {
                    if (DateTime.Now > tokenModel.ExpirationDateTime.AddDays(1))
                    {
                        db.Repository<TokenModel>().Delete(tokenModel);
                        return false;
                    }
                    userRights = tokenModel.UserRights;
                    return true;
                }
            }
            return false;
        }

    }
}