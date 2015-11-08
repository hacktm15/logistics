using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.Models;

namespace LogisticsAPI.ViewModels
{
    public class WarningViewModel
    {
        public string Message { get; set; }
        public Guid? ItemEntityId { get; set; }

        public void CopyFrom(Warning warning)
        {
            ItemEntityId = warning.ItemEntityId;
            Message = warning.Message;
        }
    }
}
