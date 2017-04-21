using System;
using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class EventConfiguration : EntityTypeConfiguration<EventEntity>
    {
        public EventConfiguration()
        {
            //Table and Columns
            this.ToTable("Events", "public");
            this.Property(_event => _event.Id)
                .HasColumnName("EventNo");
            this.Property(_event => _event.TournamentId)
                .HasColumnName("EventTournamentNo");
            this.Property(_event => _event.DateOfEvent)
                .HasColumnName("EventDate");

            //Primary Keys
            this.HasKey(_event => _event.Id);

            //Foreign Keys
            this.HasRequired<TournamentEntity>(_event => _event.Tournament)
                .WithMany(tournament => tournament.Events)
                .HasForeignKey(_event => _event.TournamentId);

            //Other Settings
            this.Property<DateTime>(_event => _event.DateOfEvent)
                .IsRequired();
            this.Property(_event => _event.Notes)
                .HasMaxLength(100);
        }
    }
}
