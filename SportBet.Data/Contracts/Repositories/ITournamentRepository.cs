using SportBet.Core.Entities;
using System;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ITournamentRepository : IRepository<TournamentEntity>
    {
        bool Exists(string name, int sportId, DateTime dateOfStart);

        TournamentEntity Get(string name, string sportName, DateTime dateOfStart);
    }
}
