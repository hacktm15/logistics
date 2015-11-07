using LogisticsAPI.DataAccess;
using LogisticsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LogisticsAPI.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Guid> Categories { get; set; }

        public Guid? LocationId { get; set; }

        public Double Quantity { get; set; }

        public Double MinQuantity { get; set; }

        public Int32 Relevance { get; set; }

        public Double Price { get; set; }

        public string Picture { get; set; }

        public void CopyFrom(Item item)
        {
            //Id = item.Id;
            EntityId = item.EntityId;
            Name = item.Name;
            Description = item.Description;
            Categories = new List<Guid>();
            foreach (var cat in item.Categories)
            {
                Categories.Add(cat.EntityId);
            }
            LocationId = item.LocationId;
            Quantity = item.Quantity;
            MinQuantity = item.MinQuantity;
            Relevance = item.Relevance;
            Price = item.Price;
            Picture = item.Picture;
        }
    }
}