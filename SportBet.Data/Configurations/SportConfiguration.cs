using SportBet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    class SportConfiguration : EntityTypeConfiguration<SportEntity>
    {
        public SportConfiguration()
        {
            //Table and Columns
            this.ToTable("Sports", "public");
            this.Property(sport => sport.Id).
                HasColumnName("SportNo");

            //Primary Keys
            this.HasKey(sport => sport.Id);

            //Other Settings
            this.Property(sport => sport.Type).
                IsRequired();
        }
    }
}
