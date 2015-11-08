using LogisticsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.DataAccess;

namespace LogisticsAPI.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Guid> Items { get; set; }

        public void CopyFrom(Category category)
        {
            //Id = category.Id;
            EntityId = category.EntityId;
            Name = category.Name;
            Description = category.Description;
            Items = new List<Guid>();
            foreach(var item in category.Items)
            {
                Items.Add(item.EntityId);
            }
        }
    }
}
