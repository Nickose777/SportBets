using SportBet.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportbookmaker.Data.Configurations
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
            this.HasKey(bookmaker => bookmaker.Id);

            //Other Settings
            this.Property(bookmaker => bookmaker.FirstName).
                IsRequired();
            this.Property(bookmaker => bookmaker.LastName).
                IsRequired();
            this.Property(bookmaker => bookmaker.PhoneNumber).
                IsRequired();
        }
    }
}
