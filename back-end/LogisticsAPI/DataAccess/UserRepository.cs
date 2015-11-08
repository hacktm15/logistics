using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.Controllers;
using LogisticsAPI.Models;
using System.DirectoryServices;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace LogisticsAPI.DataAccess
{
    public class UserRepository
    {
        String Server = "LDAP://hades.ligaac.ro:636/";
        String Domain = "dc=root";
        String ReadUser = "cn=read,dc=root";
        String ReadPassword = "1QzMmb&374<Z6W<.";

        public bool CheckUserCredentials(string user, string password, out Role[] roles)
        {
            using (var ldapController = new LDAPAuthController())
            {
                var authResponse = ldapController.IsAuthenticatedUser(user, password);
                if (!string.IsNullOrEmpty(authResponse) && authResponse.Equals("OK"))
                {
                    roles = GetUserRole(user, password, Enum.GetValues(typeof (Role)).Cast<Role>().ToArray());
                    return true;
                }
                else
                {
                    roles = null;
                    return false;
                }
            }
        }

        public static byte[] GenerateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return buff;
        }

        public static byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult =
                    new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }

        public static string GenerateSaltedSHA1(string plainTextString)
        {
            HashAlgorithm algorithm = new SHA1Managed();
            var saltBytes = GenerateSalt(4);
            var plainTextBytes = Encoding.ASCII.GetBytes(plainTextString);

            var plainTextWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes);
            var saltedSHA1Bytes = algorithm.ComputeHash(plainTextWithSaltBytes);
            var saltedSHA1WithAppendedSaltBytes = AppendByteArray(saltedSHA1Bytes, saltBytes);

            return "{SSHA}" + Convert.ToBase64String(saltedSHA1WithAppendedSaltBytes);
        }

        public bool ChangeUserInfo(string requestUser, string requestPassword,
            Dictionary<string, string> Fields)
        {
            try
            {
                string requestUserDN = GetUserDN(requestUser, requestPassword);

                DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, requestUserDN, requestPassword, AuthenticationTypes.None);
                if (UserAuth != null)
                {
                    DirectorySearcher search = new DirectorySearcher(UserAuth);

                    search.Filter = "(&(cn=" + requestUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();
                        foreach (KeyValuePair<string, string> field in Fields)
                        {
                            if (field.Key.Equals("userPassword"))
                            {
                                String hashedPassword = GenerateSaltedSHA1(field.Value);
                                userEntry.Properties["userPassword"].Value = hashedPassword;
                            }
                            else
                            {
                                userEntry.Properties[field.Key].Value = field.Value;
                            }
                        }

                        userEntry.CommitChanges();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public Dictionary<string, string> GetUserInfo(string requestUser, string requestPassword, string targetUser,
            string[] fields)
        {
            User user = new User();

            try
            {
                String requestDNUser = GetUserDN(ReadUser, ReadPassword, requestUser);
                DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, requestDNUser, requestPassword, AuthenticationTypes.None);

                DirectorySearcher search = new DirectorySearcher(UserAuth);

                search.Filter = "(&(cn=" + targetUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    foreach (String Field in fields)
                    {
                        switch (Field)
                        {
                            case "displayName":
                                user.Name = result.Properties[Field][0].ToString();
                                break;
                            case "mail":
                                user.Mail = result.Properties[Field][0].ToString();
                                break;
                            case "telephoneNumber":
                                user.Phone = result.Properties[Field][0].ToString();
                                break;
                            case "title":
                                user.Title = result.Properties[Field][0].ToString();
                                break;
                            default:
                                Console.WriteLine("Field doesn't exist.");
                                break;
                        }
                    }
                }
                else
                {
                    return new Dictionary<string, string> {{"Error", "User not found!"}};
                }

                Dictionary<string, string> UserInfo = new Dictionary<string, string>();
                UserInfo.Add("Name", user.Name);
                UserInfo.Add("Mail", user.Mail);
                UserInfo.Add("Title", user.Title);
                UserInfo.Add("Phone", user.Phone);

                return UserInfo;
            }
            catch (Exception ex)
            {
                Dictionary<string, string> ErrorInfo = new Dictionary<string, string>();
                ErrorInfo.Add("Error", "Error getting user info." + ex);

                return ErrorInfo;
            }
        }

        public Role[] GetUserRole(string requestUser, string requestPassword, Role[] roles, string targetUser = "self")
        {
            List<Role> foundRoles = new List<Role>() { Role.Self };
            if (targetUser == "self")
                targetUser = requestUser;

            try
            {
                string requestUserDN = GetUserDN(requestUser, requestPassword);

                DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, requestUserDN, requestPassword, AuthenticationTypes.None);
                DirectorySearcher UserAuthSearch = new DirectorySearcher(UserAuth);
                foreach (Role role in roles)
                {
                    UserAuthSearch.Filter = "(&(cn=" + requestUser + "*)(memberOf=cn=" + role.ToString() + ",cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                    SearchResult UserAuthResult = UserAuthSearch.FindOne();

                    if(UserAuthResult != null)
                    {
                        if (!foundRoles.Contains(role))
                        {
                            foundRoles.Add(role);
                        }
                    }
                }

                return foundRoles.ToArray();
            }
            catch (Exception)
            {
                return new Role[] { Role.None };
            }
            
        }

        public string GetUserDN(string requestUser, string requestPassword, string targetUser = "self")
        {
            String DNUser = "";
            DirectoryEntry LDAP = new DirectoryEntry(Server + Domain, ReadUser, ReadPassword, AuthenticationTypes.None);

            if (targetUser == "self")
                targetUser = requestUser;

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP);
                search.Filter = "(&(cn=" + targetUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                SearchResult result = search.FindOne();
                DNUser = result.Path.Substring(Server.Length);
                return DNUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting User DN.", ex);
            }
        }
    }
}