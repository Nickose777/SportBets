﻿using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class TournamentRepository : RepositoryBase<TournamentEntity>, IRepository<TournamentEntity>
    {
        public TournamentRepository(SportBetDbContext context)
            : base(context) { }
    }
}
