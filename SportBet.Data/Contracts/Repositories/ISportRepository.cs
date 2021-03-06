﻿using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ISportRepository : IRepository<SportEntity>
    {
        bool Exists(string sportName);

        SportEntity Get(string sportName);
    }
}
