﻿using System;
using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class TournamentConfiguration : EntityTypeConfiguration<TournamentEntity>
    {
        public TournamentConfiguration()
        {
            //Table and Columns
            this.ToTable("Tournaments", "public");
            this.Property(tournament => tournament.Id)
                .HasColumnName("TournamentNo");
            this.Property(tournament => tournament.SportId)
                .HasColumnName("TournamentSportNo");

            //Primary Keys
            this.HasKey(tournament => tournament.Id);

            //Foreign Keys
            this.HasRequired<SportEntity>(tournament => tournament.Sport)
                .WithMany(sport => sport.Tournaments)
                .HasForeignKey(tournament => tournament.SportId);

            //Other Settings
            this.Property(tournament => tournament.Name)
                .IsRequired()
                .HasMaxLength(20);
            this.Property<DateTime>(tournament => tournament.DateOfStart)
                .IsRequired();
        }
    }
}