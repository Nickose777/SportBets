using SportBet.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    class CoefficientConfiguration : EntityTypeConfiguration<CoefficientEntity>
    {
        public CoefficientConfiguration()
        {
            //Table and Columns
            this.ToTable("Coefficients", "public");
            this.Property(coefficient => coefficient.Id).
                HasColumnName("CoefficientNo");
            this.Property(coefficient => coefficient.EventId).
                HasColumnName("CoefficientEventNo");

            //Primary Keys
            this.HasKey(coefficient => coefficient.Id);

            //Foreign Keys
            this.HasRequired<EventEntity>(coefficient => coefficient.Event).
                WithMany(_event => _event.Coefficients).
                HasForeignKey(coefficient => coefficient.EventId);

            //Other Settings
            this.Property(coefficient => coefficient.Description).
                IsRequired().
                HasMaxLength(100);
            this.Property<decimal>(coefficient => coefficient.Value).
                IsRequired();
        }
    }
}
