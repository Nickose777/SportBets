using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class SportRepository : RepositoryBase<SportEntity>, ISportRepository
    {
        public SportRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public SportEntity Get(string sportName)
        {
            SportBetDbContext context = GetContext();
            SportEntity sportEntity = context.Sports.SingleOrDefault(sport => sport.Type == sportName);

            return sportEntity;
        }
    }
}
