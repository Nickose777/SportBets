using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class SportRepository : RepositoryBase<SportEntity>, ISportRepository
    {
        public SportRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
