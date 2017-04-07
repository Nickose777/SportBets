using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class AdminConfiguration : EntityTypeConfiguration<AdminEntity>
    {
        public AdminConfiguration()
        {
            //Table and Columns
            this.ToTable("Admins", "public");
            this.Property(admin => admin.Id)
                .HasColumnName("AdminNo");

            //Primary Keys
            this.HasKey(admin => admin.Id)
                .Property(admin => admin.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(admin => admin.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(admin => admin.LastName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(admin => admin.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
