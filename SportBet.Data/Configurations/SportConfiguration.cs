using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class SportConfiguration : EntityTypeConfiguration<SportEntity>
    {
        public SportConfiguration()
        {
            //Table and Columns
            this.ToTable("Sports", "public");
            this.Property(sport => sport.Id)
                .HasColumnName("SportNo");

            //Primary Keys
            this.HasKey(sport => sport.Id);

            //Other Settings
            this.Property(sport => sport.Type)
                .IsRequired();
        }
    }
}
