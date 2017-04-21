using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System.Data.Entity;
using System.Collections.Generic;

namespace SportBet.Data.Repositories
{
    class EventRepository : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
