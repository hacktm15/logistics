using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.Controllers;
using LogisticsAPI.Models;

namespace LogisticsAPI.DataAccess
{
    public class UserRepository
    {
        public static bool CheckUserCredentials(string user, string password, out UserRights userRights)
        {
            userRights = UserRights.SmecherUser;
            using (var ldapController = new LDAPAuthController())
            {
                var authResponse = ldapController.IsAuthenticatedUser(user, password);
                if (!string.IsNullOrEmpty(authResponse) && authResponse.Equals("OK"))
                {
                    userRights = UserRights.SmecherUser;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
