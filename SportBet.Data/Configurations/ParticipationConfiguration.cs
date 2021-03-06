﻿using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class ParticipationConfiguration : EntityTypeConfiguration<ParticipationEntity>
    {
        public ParticipationConfiguration()
        {
            //Table and Columns
            this.ToTable("Participations", "public");
            this.Property(participation => participation.EventId)
                .HasColumnName("ParticipationEventNo");
            this.Property(participation => participation.ParticipantId)
                .HasColumnName("ParticipationParticipantNo");

            //Primary Keys
            this.HasKey(participation => new 
            {
                participation.EventId,
                participation.ParticipantId 
            });

            //Foreign Keys
            this.HasRequired<EventEntity>(participation => participation.Event)
                .WithMany(_event => _event.Participations)
                .HasForeignKey(participation => participation.EventId);
            this.HasRequired<ParticipantEntity>(participation => participation.Participant)
                .WithMany(participant => participant.Participations)
                .HasForeignKey(participation => participation.ParticipantId);
        }
    }
}
