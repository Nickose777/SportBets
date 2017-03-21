﻿using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class ParticipationRepository : RepositoryBase<ParticipationEntity>, IRepository<ParticipationEntity>
    {
        public ParticipationRepository(SportBetDbContext context)
            : base(context) { }

        public override ParticipationEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
