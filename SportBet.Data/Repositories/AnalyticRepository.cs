using System;
using System.Collections.Generic;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class AnalyticRepository : RepositoryBase<AnalyticEntity>, IAnalyticRepository
    {
        public AnalyticRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public IEnumerable<AnalyticEntity> GetNotDeleted()
        {
            return GetAll(analytic => !analytic.IsDeleted);
        }
    }
}
