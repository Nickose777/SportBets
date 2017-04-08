using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class AdminPasswordConfiguration : EntityTypeConfiguration<AdminPasswordEntity>
    {
        public AdminPasswordConfiguration()
        {
            //Table and Columns
            this.ToTable("AdminPassword", "public");
            this.Property(adminPassword => adminPassword.Id)
                .HasColumnName("AdminPasswordNo");

            //Primary Keys
            this.HasKey(adminPassword => adminPassword.Id);

            //Other Settings
            this.Property(adminPassword => adminPassword.Value)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
