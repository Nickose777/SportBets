using SportBet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    class ClientConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientConfiguration()
        {
            //Table and Columns
            this.ToTable("Clients", "public");
            this.Property(client => client.Id).
                HasColumnName("ClientNo");

            //Primary Keys
            this.HasKey(client => client.Id).
                Property(client => client.Id).
                HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            //Other Settings
            this.Property(client => client.FirstName).
                IsRequired().
                HasMaxLength(20);
            this.Property(client => client.LastName).
                IsRequired().
                HasMaxLength(20);
            this.Property(client => client.PhoneNumber).
                IsRequired().
                HasMaxLength(20);
            this.Property<DateTime>(client => client.DateOfBirth).
                IsRequired();
            this.Property<DateTime>(client => client.DateOfRegistration).
                IsRequired();
        }
    }
}
