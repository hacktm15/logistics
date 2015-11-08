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
        public TokenModel GenerateAndRegisterTokenForUserWithRights(string username, string password, Role[] roles)
        {
            var tokenModel = new TokenModel
            {
                Roles = string.Join(",", roles),
                Username = username,
                ExpirationDateTime = DateTime.Now.AddDays(1),
                Token =
                    Convert.ToBase64String(
                        Encoding.Default.GetBytes(username + "_" + Guid.NewGuid())),
                Password = password
            };
            using (var db = new DBUnitOfWork())
            {
                db.Repository<TokenModel>().Add(tokenModel);
            }
            return tokenModel;
        }

        public bool IsTokenValid(string token, out Role[] role)
        {
            using (var db = new DBUnitOfWork())
            {
                var tokenModel = db.Repository<TokenModel>().Find(x => x.Token.Equals(token));
                if (tokenModel != null)
                {
                    if (DateTime.Now > tokenModel.ExpirationDateTime.AddDays(1))
                    {
                        db.Repository<TokenModel>().Delete(tokenModel);
                    }
                    else
                    {
                        role =
                            tokenModel.Roles.Split(',')
                                .Select(roleStr => (Role) Enum.Parse(typeof (Role), roleStr))
                                .ToArray();
                        return true;
                    }
                }
            }
            role = new[] {Role.None};
            return false;
        }

        public string GetUserNameFromToken(string token)
        {
            var decoded = Encoding.Default.GetString(Convert.FromBase64String(token));
            var userName = decoded.Split('_')[1];
            return userName;
        }

        public AuthRequest GetAuthRequestFromToken(string token)
        {
            using (var db = new DBUnitOfWork())
            {
                var tokenModel = db.Repository<TokenModel>().Find(x => x.Token.Equals(token));
                return new AuthRequest()
                {
                    User = tokenModel.Username,
                    Password = tokenModel.Password
                };
            }
        }

        public void InvalidateTokensForUser(string userName, string token)
        {
            if (token == null)
            {
                return;
            }
            using (var db = new DBUnitOfWork())
            {
                var tokens = db.Repository<TokenModel>().FindAll(x => x.Username.Equals(userName));
                foreach (var oldToken in tokens.Where(x => !x.Token.Equals(token)))
                {
                    db.Repository<TokenModel>().Delete(oldToken);
                }
            }
        }
    }
}