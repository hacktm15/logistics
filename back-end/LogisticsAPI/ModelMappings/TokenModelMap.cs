using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsAPI.Models;

namespace LogisticsAPI.ModelMappings
{
    public class TokenModelMap : EntityTypeConfiguration<TokenModel>
    {
        public TokenModelMap()
        {
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(t => t.EntityId);
        }
    }
}
