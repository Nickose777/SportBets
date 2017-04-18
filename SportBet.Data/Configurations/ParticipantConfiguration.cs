using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class ParticipantConfiguration : EntityTypeConfiguration<ParticipantEntity>
    {
        public ParticipantConfiguration()
        {
            //Table and Columns
            this.ToTable("Participants", "public");
            this.Property(participant => participant.Id)
                .HasColumnName("ParticipantNo");
            this.Property(participant => participant.CountryId)
                .HasColumnName("ParticipantCountryNo");
            this.Property(participant => participant.SportId)
                .HasColumnName("ParticipantSportNo");

            //Primary Keys
            this.HasKey(participant => participant.Id);

            //Foreign Keys
            this.HasRequired<CountryEntity>(participant => participant.Country)
                .WithMany(client => client.Participants)
                .HasForeignKey(participant => participant.CountryId);
            this.HasRequired<SportEntity>(participant => participant.Sport)
                .WithMany(sport => sport.Participants)
                .HasForeignKey(participant => participant.SportId);

            //Other Settings
            this.Property(participant => participant.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
