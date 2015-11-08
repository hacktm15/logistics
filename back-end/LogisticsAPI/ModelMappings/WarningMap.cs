using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LogisticsAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAPI.ModelMappings
{
    public class WarningMap : EntityTypeConfiguration<Warning>
    {
        public WarningMap()
        {
            //HasKey(t => t.Id);
            //Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(t => t.ItemEntityId);

            //MapToStoredProcedures();
        }
    }
}
