using SportBet.Core.Entities;
using System;
using System.Collections.Generic;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IEventRepository : IRepository<EventEntity>
    {
        EventEntity Get(string sportName, string tournamentName, DateTime dateOfEvent, IEnumerable<ParticipantEntity> participantEntities);
    }
}
