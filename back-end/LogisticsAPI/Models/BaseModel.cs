using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsAPI.Models
{
    public class BaseModel
    {
        //public Int64 Id { get; set; }
        public Guid EntityId { get; set; }
    }
}