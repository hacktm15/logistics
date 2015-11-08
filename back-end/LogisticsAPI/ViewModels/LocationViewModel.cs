using LogisticsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsAPI.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Guid> Items { get; set; }

        public void CopyFrom(Location location)
        {
            //Id = location.Id;
            EntityId = location.EntityId;
            Name = location.Name;
            Description = location.Description;
            Items = new List<Guid>();
            foreach (var item in location.Items)
            {
                Items.Add(item.EntityId);
            }
        }
    }
}