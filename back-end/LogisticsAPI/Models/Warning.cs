using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.DataAccess;
using LogisticsAPI.ViewModels;

namespace LogisticsAPI.Models
{
    public class Warning : BaseModel
    {
        public string Message { get; set; }
        public Guid ItemEntityId { get; set; }
        public virtual Item Item { get; set; }

        public void CopyFrom(WarningViewModel itemViewModel, DBUnitOfWork db)
        {
            Message = itemViewModel.Message;
            if (itemViewModel.ItemEntityId != null)
            {
                ItemEntityId = itemViewModel.ItemEntityId.Value;
            }
            else
            {
                throw new Exception("This Warning doesn't have an associated Item!");
            }
        }
    }
}
