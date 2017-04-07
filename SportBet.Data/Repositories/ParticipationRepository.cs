using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class ParticipationRepository : RepositoryBase<ParticipationEntity>, IParticipationRepository
    {
        public ParticipationRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public override ParticipationEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
