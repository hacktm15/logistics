using LogisticsAPI.DataAccess;
using LogisticsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public Category()
        {

        }

        public void CopyFrom(CategoryViewModel categoryViewModel, DBUnitOfWork db)
        {
            Name = categoryViewModel.Name;
            Description = categoryViewModel.Description;
            Items = new List<Item>();
            if (categoryViewModel.Items != null)
            {
                foreach (var itemId in categoryViewModel.Items)
                {
                    var existing = db.Repository<Item>().Get(itemId);
                    if (existing != null)
                    {
                        Items.Add(existing);
                    }
                }
            }
        }
    }
}
