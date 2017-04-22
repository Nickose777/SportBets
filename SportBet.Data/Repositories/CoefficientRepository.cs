using System;
using System.Linq;
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

        public bool Exists(int eventId, decimal value, string description)
        {
            SportBetDbContext context = GetContext();

            CoefficientEntity coefficientEntity = context
                .Coefficients.SingleOrDefault(c => 
                    c.EventId == eventId && 
                    c.Value == value &&
                    c.Description == description);

            return coefficientEntity != null;
        }
    }
}
