using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public enum Role
    {
        Admin,
        Write,
        Read,
        Self,
        None
    }
}