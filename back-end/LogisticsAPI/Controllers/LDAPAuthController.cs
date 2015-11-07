using System;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Web.Http;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json;
using LogisticsAPI.Models;

namespace LogisticsAPI.Controllers
{
    public class LDAPAuthController : ApiController
    {
        String Server = "LDAP://hades.ligaac.ro:636/";
        String Domain = "dc=root";
        String ReadUser = "cn=read,dc=root";
        String ReadPassword = "1QzMmb&374<Z6W<.";

        [HttpGet]
        [Route("api/LDAPAuth/IsAuthenticatedUser")]
        public String IsAuthenticatedUser(String AuthUser, String AuthPassword = "[02Batman]")
        {
            String FullAuthUser = "";
            DirectoryEntry LDAP = new DirectoryEntry(Server + Domain, ReadUser, ReadPassword, AuthenticationTypes.None);

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP);

                search.Filter = "(cn=" + AuthUser + "*)";
                SearchResult result = search.FindOne();
                FullAuthUser = result.Path.Substring(Server.Length);
                DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, FullAuthUser, AuthPassword, AuthenticationTypes.None);
                DirectorySearcher UserAuthSearch = new DirectorySearcher(UserAuth);          
                UserAuthSearch.Filter = "(&(cn=" + AuthUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                SearchResult UserAuthResult = UserAuthSearch.FindOne();
                //return "User: " + FullAuthUser + "\nPassword: " + System.Text.Encoding.UTF8.GetString(UserAuthResult.Properties["userPassword"][0] as byte[]);
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error authenticating user. The user name or password is incorrect.\n" + ex.Message;
            }
        }

        [HttpGet]
        [Route("api/LDAPAuth/GetUserData")]
        public String GetUserData(String DNUser, [FromUri] String[] UserFields, String AuthPassword = "[02Batman]")
        {
            DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, DNUser, AuthPassword, AuthenticationTypes.None);
            String UserName = "";
            String UserEmail = "";
            String UserPhone = "";
            String UserTitle = "";
            try
            {
                DirectorySearcher search = new DirectorySearcher(UserAuth);

                string CNUser = DNUser.Split(',')[0].Split('=')[1];
                search.Filter = "(&(cn=" + CNUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                SearchResult result = search.FindOne();
                foreach(String Field in UserFields)
                {
                    switch (Field)
                    {
                        case "givenName":
                            UserName = result.Properties[Field][0].ToString();
                            break;
                        case "mail":
                            UserEmail = result.Properties[Field][0].ToString();
                            break;
                        case "telephoneNumber":
                            UserPhone = result.Properties[Field][0].ToString();
                            break;
                        case "title":
                            UserTitle = result.Properties[Field][0].ToString();
                            break;
                        default:
                            Console.WriteLine("Field doesn't exist.");
                            break;
                    }
                }
                var user = new User { Name = UserName, Mail = UserEmail, Phone = UserPhone, Title = UserTitle };
                var jsonUser = JsonConvert.SerializeObject(user);
                return jsonUser;
            }
            catch (Exception ex)
            {
                return "Error authenticating user. The user name or password is incorrect.\n" + ex.Message;
            }
        }

        [HttpGet]
        [Route("api/LDAPAuth/ChangeUserField")]
        public string ChangeUserField(string DNUser, String Field, String AuthPassword = "[01Batman]", string NewFieldValue = "[02Batman]")
        {
            try
            {
                DirectoryEntry UserAuth = new DirectoryEntry(Server + Domain, DNUser, AuthPassword, AuthenticationTypes.None);
                if (UserAuth != null)

                    {
             
                    DirectorySearcher search = new DirectorySearcher(UserAuth);

                    string CNUser = DNUser.Split(',')[0].Split('=')[1];
                    search.Filter = "(&(cn=" + CNUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();
                        if (userEntry != null)
                        {
                            if (Field.Equals("userPassw0rd"))
                            {
                                String hashedPassword = GenerateSaltedSHA1(NewFieldValue);
                                userEntry.Properties["userPassword"].Value = hashedPassword;
                            }
                            else
                            {
                                userEntry.Properties[Field].Value = NewFieldValue;
                            }
                            
                            userEntry.CommitChanges();                       
                         
                        }
                    }
                }
                return "Password changed.";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }

        [HttpGet]
        [Route("api/LDAPAuth/GetDNUser")]
        public String GetDNUser(String CNUser)
        {
            String DNUser = "";
            DirectoryEntry LDAP = new DirectoryEntry(Server + Domain, ReadUser, ReadPassword, AuthenticationTypes.None);

            try
            {
                DirectorySearcher search = new DirectorySearcher(LDAP);
                search.Filter = "(&(cn=" + CNUser + "*)(memberOf=cn=logistics,dc=services,dc=groups,dc=liga.ac,dc=root))";
                SearchResult result = search.FindOne();
                DNUser = result.Path.Substring(Server.Length);
                return DNUser;
            }
            catch (Exception ex)
            {
                return "User not found." + ex.Message;
            }
        }

        [HttpGet]
        [Route("api/LDAPAuth/GenerateSaltedSHA1")]
        public string GenerateSaltedSHA1([FromUri]string plainTextString)
        {
            HashAlgorithm algorithm = new SHA1Managed();
            var saltBytes = GenerateSalt(4);
            var plainTextBytes = Encoding.ASCII.GetBytes(plainTextString);

            var plainTextWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes);
            var saltedSHA1Bytes = algorithm.ComputeHash(plainTextWithSaltBytes);
            var saltedSHA1WithAppendedSaltBytes = AppendByteArray(saltedSHA1Bytes, saltBytes);

            return "{SSHA}" + Convert.ToBase64String(saltedSHA1WithAppendedSaltBytes);
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

    }
}
