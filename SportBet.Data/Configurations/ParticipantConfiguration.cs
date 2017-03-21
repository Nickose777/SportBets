using SportBet.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Configurations
{
    class ParticipantConfiguration : EntityTypeConfiguration<ParticipantEntity>
    {
        public ParticipantConfiguration()
        {
            //Table and Columns
            this.ToTable("Participants", "public");
            this.Property(participant => participant.Id).
                HasColumnName("ParticipantNo");
            this.Property(participant => participant.CountryId).
                HasColumnName("ParticipantCountryNo");

            //Primary Keys
            this.HasKey(participant => participant.Id);

            //Foreign Keys
            this.HasRequired<CountryEntity>(participant => participant.Country).
                WithMany(client => client.Participants).
                HasForeignKey(participant => participant.CountryId);

            //Other Settings
            this.Property(participant => participant.Name).
                IsRequired();
        }
    }
}
