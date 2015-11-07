using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LogisticsAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAPI.ModelMappings
{
    public class ItemMap: EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            //HasKey(t => t.Id);
            //Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(t => t.EntityId);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(i => i.Location)
                .WithMany(t=>t.Items)
                .HasForeignKey(l => l.LocationId);

            HasMany(t => t.Categories).WithMany(t => t.Items).Map(m =>
            {
                m.ToTable("ItemsCategoryBinding");
                m.MapLeftKey("ItemEntityId");
                m.MapRightKey("CategoryEntityId");
            });

            //MapToStoredProcedures();
        }
    }
}