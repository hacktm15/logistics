using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LogisticsAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAPI.ModelMappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            //HasKey(t => t.EntityId);
            //Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(t => t.EntityId);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MapToStoredProcedures();
        }
    }   
}