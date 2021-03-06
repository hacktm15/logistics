﻿using System;
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
        public string Roles { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public string Password { get; set; }
    }
}