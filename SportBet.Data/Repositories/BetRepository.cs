using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class BetRepository : RepositoryBase<BetEntity>, IBetRepository
    {
        public BetRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public override BetEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
