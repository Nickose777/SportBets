using SportBet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    class BookmakerConfiguration : EntityTypeConfiguration<BookmakerEntity>
    {
        public BookmakerConfiguration()
        {
            //Table and Columns
            this.ToTable("Bookmakers", "public");
            this.Property(bookmaker => bookmaker.Id).
                HasColumnName("BookmakerNo");

            //Primary Keys
            this.HasKey(bookmaker => bookmaker.Id).
                Property(bookmaker => bookmaker.Id).
                HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(bookmaker => bookmaker.FirstName).
                IsRequired().
                HasMaxLength(20);
            this.Property(bookmaker => bookmaker.LastName).
                IsRequired().
                HasMaxLength(20);
            this.Property(bookmaker => bookmaker.PhoneNumber).
                IsRequired().
                HasMaxLength(20);
        }
    }
}
