using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class EventRepository : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
