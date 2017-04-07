using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class CountryConfiguration : EntityTypeConfiguration<CountryEntity>
    {
        public CountryConfiguration()
        {
            //Table and Columns
            this.ToTable("Countries", "public");
            this.Property(country => country.Id)
                .HasColumnName("CountryNo");

            //Primary Keys
            this.HasKey(country => country.Id);

            //Other Settings
            this.Property(country => country.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
