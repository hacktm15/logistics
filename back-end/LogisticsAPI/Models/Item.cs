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
    public class Item : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public Guid LocationId { get; set; }

        public virtual Location Location { get; set; }

        public Double Quantity { get; set; }

        public Double MinQuantity { get; set; }

        public Int32 Relevance { get; set; }

        public Double Price { get; set; }

        public string Picture { get; set; }

        public virtual Warning Warning { get; set; }

        public string Status { get; set; }

        public void CopyFrom(ItemViewModel itemViewModel, DBUnitOfWork db)
        {
            //Id = itemViewModel.Id;
            Name = itemViewModel.Name;
            Description = itemViewModel.Description;
            Status = itemViewModel.Status;
            
            Location = db.Repository<Location>().Get(LocationId);
            if (itemViewModel.LocationId != null)
            {
                LocationId = itemViewModel.LocationId.Value;
            }
            else
            {
                //LocationId = itemViewModel.LocationId ?? new Guid("803ad3c6-cd87-4ad8-9a26-3675f4999f42");
                var defaultLocation = db.Repository<Location>().Find(x => x.Name == "Unknown");
                if (defaultLocation != null)
                {
                    LocationId = defaultLocation.EntityId;
                }
                else
                {
                    throw new Exception("You don't have the 'Unknown' location.");
                }
            }
            Quantity = itemViewModel.Quantity;
            MinQuantity = itemViewModel.MinQuantity;
            Relevance = itemViewModel.Relevance;
            Price = itemViewModel.Price;
            Picture = itemViewModel.Picture;
            
            Categories = new List<Category>();
            if (itemViewModel.Categories != null)
            {
                foreach (var catId in itemViewModel.Categories)
                {
                    var existing = db.Repository<Category>().Get(catId);
                    if (existing != null)
                    {
                        Categories.Add(existing);
                    }
                }
            }
        }
    }
}
