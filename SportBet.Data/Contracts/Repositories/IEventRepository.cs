using System;
using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IEventRepository : IRepository<EventEntity>
    {
        bool Exists(DateTime dateOfEvent, int tournamentId);
    }
}
