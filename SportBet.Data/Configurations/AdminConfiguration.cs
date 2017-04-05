using SportBet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    //TODO
    //Refactore dots
    class AdminConfiguration : EntityTypeConfiguration<AdminEntity>
    {
        public AdminConfiguration()
        {
            //Table and Columns
            this.ToTable("Admins", "public");
            this.Property(admin => admin.Id).
                HasColumnName("AdminNo");

            //Primary Keys
            this.HasKey(admin => admin.Id).
                Property(admin => admin.Id).
                HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(admin => admin.FirstName).
                IsRequired().
                HasMaxLength(20);
            this.Property(admin => admin.LastName).
                IsRequired().
                HasMaxLength(20);
            this.Property(admin => admin.PhoneNumber).
                IsRequired().
                HasMaxLength(15);
        }
    }
}
