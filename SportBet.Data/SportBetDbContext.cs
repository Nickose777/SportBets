using SportBet.Core.Entities;
using SportBet.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data
{
    public class SportBetDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<BetEntity> Bets { get; set; }
        public DbSet<BookmakerEntity> Bookmakers { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CoefficientEntity> Coefficients { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }
        public DbSet<ParticipationEntity> Participations { get; set; }
        public DbSet<SportEntity> Sports { get; set; }
        public DbSet<TournamentEntity> Tournaments { get; set; }

        public SportBetDbContext()
            : base("Server=127.0.0.1;Port=5432;Database=Bets;User Id=admin;Password=admin;") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Configurations.Add(new RoleConfiguraion());
            modelBuilder.Configurations.Add(new UserConfiguraion());

            modelBuilder.Configurations.Add(new BetConfiguration());
            modelBuilder.Configurations.Add(new BookmakerConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new CoefficientConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new ParticipantConfiguration());
            modelBuilder.Configurations.Add(new ParticipationConfiguration());
            modelBuilder.Configurations.Add(new SportConfiguration());
            modelBuilder.Configurations.Add(new TournamentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
