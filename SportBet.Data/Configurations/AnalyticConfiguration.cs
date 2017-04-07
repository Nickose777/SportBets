using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class AnalyticConfiguration : EntityTypeConfiguration<AnalyticEntity>
    {
        public AnalyticConfiguration()
        {
            //Table and Columns
            this.ToTable("Analytics", "public");
            this.Property(analytic => analytic.Id)
                .HasColumnName("AnalyticNo");

            //Primary Keys
            this.HasKey(analytic => analytic.Id)
                .Property(analytic => analytic.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(analytic => analytic.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(analytic => analytic.LastName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(analytic => analytic.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
