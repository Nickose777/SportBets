using System;
using SportBet.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SportBet.Data.Configurations
{
    class BetConfiguration : EntityTypeConfiguration<BetEntity>
    {
        public BetConfiguration()
        {
            //Table and Columns
            this.ToTable("Bets", "public");
            this.Property(bet => bet.ClientId).HasColumnName("BetClientNo");
            this.Property(bet => bet.CoefficientId).HasColumnName("BetCoefficientNo");
            this.Property(bet => bet.BookmakerId).HasColumnName("BetBookmakerNo");

            //Primary Keys
            this.HasKey(bet => new 
            { 
                bet.ClientId,
                bet.CoefficientId
            });

            //Foreign Keys
            this.HasRequired<ClientEntity>(bet => bet.Client)
                .WithMany(client => client.Bets)
                .HasForeignKey(bet => bet.ClientId);
            this.HasRequired<CoefficientEntity>(bet => bet.Coefficient)
                .WithMany(coefficient => coefficient.Bets)
                .HasForeignKey(bet => bet.CoefficientId);
            this.HasRequired<BookmakerEntity>(bet => bet.Bookmaker)
                .WithMany(bookmaker => bookmaker.Bets)
                .HasForeignKey(bet => bet.BookmakerId);

            //Other Settings
            this.Property<DateTime>(bet => bet.RegistrationDate)
                .IsRequired();
            this.Property<decimal>(bet => bet.Sum)
                .IsRequired();
        }
    }
}
