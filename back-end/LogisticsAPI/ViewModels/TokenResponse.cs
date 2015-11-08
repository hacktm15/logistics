using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public class TokenResponse
    {
        public string UserRights { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
