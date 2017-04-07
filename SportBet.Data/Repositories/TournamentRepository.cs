using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class TournamentRepository : RepositoryBase<TournamentEntity>, ITournamentRepository
    {
        public TournamentRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
