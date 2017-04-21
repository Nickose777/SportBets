using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System.Data.Entity;

namespace SportBet.Data.Repositories
{
    class EventRepository : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public bool Exists(DateTime dateOfEvent, int tournamentId)
        {
            SportBetDbContext context = GetContext();
            EventEntity eventEntity = context
                .Events
                .SingleOrDefault(e =>
                    e.TournamentId == tournamentId &&
                    DbFunctions.TruncateTime(e.DateOfEvent) == dateOfEvent.Date
                    );

            return eventEntity != null;
        }
    }
}
