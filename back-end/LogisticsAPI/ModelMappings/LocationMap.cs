using LogisticsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsAPI.ModelMappings
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            //HasKey(t => t.Id);
            //Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(t => t.EntityId);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //MapToStoredProcedures();
        }
    }
}
