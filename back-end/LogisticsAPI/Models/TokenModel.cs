using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public class TokenModel : BaseModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public UserRights UserRights { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}