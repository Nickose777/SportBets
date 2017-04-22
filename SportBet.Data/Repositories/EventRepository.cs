using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class EventRepository : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public EventEntity Get(string sportName, string tournamentName, DateTime dateOfEvent, IEnumerable<ParticipantEntity> participantEntities)
        {
            SportBetDbContext context = GetContext();

            var participantIds = participantEntities.Select(p => p.Id);

            EventEntity eventEntity = context
                .Events
                .SingleOrDefault(_event => 
                    _event.Tournament.Sport.Type == sportName &&
                    _event.Tournament.Name == tournamentName &&
                    DbFunctions.TruncateTime(_event.DateOfEvent) == dateOfEvent &&
                    _event.Participations.Select(part => part.ParticipantId).All(id => participantIds.Contains(id)));

            return eventEntity;
        }
    }
}
