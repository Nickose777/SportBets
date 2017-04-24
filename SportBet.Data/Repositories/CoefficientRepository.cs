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

        public bool Exists(int eventId, string description)
        {
            CoefficientEntity coefficientEntity = Get(eventId, description);

            return coefficientEntity != null;
        }

        public CoefficientEntity Get(int eventId, string description)
        {
            SportBetDbContext context = GetContext();

            CoefficientEntity coefficientEntity = context
                .Coefficients.SingleOrDefault(c =>
                    c.EventId == eventId &&
                    c.Description == description);

            return coefficientEntity;
        }
    }
}
