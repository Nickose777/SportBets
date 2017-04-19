using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class TournamentRepository : RepositoryBase<TournamentEntity>, ITournamentRepository
    {
        public TournamentRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public bool Exists(string name, int sportId, DateTime dateOfStart)
        {
            SportBetDbContext context = GetContext();
            TournamentEntity tournament = context
                .Tournaments
                .SingleOrDefault(t =>
                    t.Name == name && 
                    t.SportId == sportId &&
                    t.DateOfStart == dateOfStart);

            return tournament != null;
        }

        public TournamentEntity Get(string name, string sportName, DateTime dateOfStart)
        {
            SportBetDbContext context = GetContext();
            TournamentEntity tournament = context
                .Tournaments
                .SingleOrDefault(t =>
                    t.Name == name &&
                    t.Sport.Type == sportName &&
                    t.DateOfStart == dateOfStart);

            return tournament;
        }
    }
}
