using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class CoefficientRepository : RepositoryBase<CoefficientEntity>, ICoefficientRepository
    {
        public CoefficientRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public override CoefficientEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
