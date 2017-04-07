using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class AnalyticRepository : RepositoryBase<AnalyticEntity>, IAnalyticRepository
    {
        public AnalyticRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
