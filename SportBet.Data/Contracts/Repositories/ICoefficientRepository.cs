﻿using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ICoefficientRepository : IRepository<CoefficientEntity>
    {
        bool Exists(int eventId, string description);

        CoefficientEntity Get(int eventId, string description);
    }
}
