using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class BookmakerConfiguration : EntityTypeConfiguration<BookmakerEntity>
    {
        public BookmakerConfiguration()
        {
            //Table and Columns
            this.ToTable("Bookmakers", "public");
            this.Property(bookmaker => bookmaker.Id)
                .HasColumnName("BookmakerNo");

            //Primary Keys
            this.HasKey(bookmaker => bookmaker.Id)
                .Property(bookmaker => bookmaker.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(bookmaker => bookmaker.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(bookmaker => bookmaker.LastName)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(bookmaker => bookmaker.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
