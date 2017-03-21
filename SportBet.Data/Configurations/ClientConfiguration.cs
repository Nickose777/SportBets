using SportBet.Core.Models;
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
            this.HasKey(client => client.Id);

            //Other Settings
            this.Property(client => client.FirstName).
                IsRequired();
            this.Property(client => client.LastName).
                IsRequired();
            this.Property(client => client.PhoneNumber).
                IsRequired();
            this.Property<DateTime>(client => client.DateOfBirth).
                IsRequired();
            this.Property<DateTime>(client => client.DateOfRegistration).
                IsRequired();
        }
    }
}
