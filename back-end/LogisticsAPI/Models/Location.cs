using LogisticsAPI.DataAccess;
using LogisticsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public class Location : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        
        public void CopyFrom(LocationViewModel locationViewModel, DBUnitOfWork db)
        {
            //Id = locationViewModel.Id;
            Name = locationViewModel.Name;
            Description = locationViewModel.Description;

            Items = new List<Item>();
            if (locationViewModel.Items != null)
            {
                foreach (var itemId in locationViewModel.Items)
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
