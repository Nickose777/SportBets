using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class EventRepository : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
