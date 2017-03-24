using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class ParticipantRepository : RepositoryBase<ParticipantEntity>, IParticipantRepository
    {
        public ParticipantRepository(SportBetDbContext context)
            : base(context) { }
    }
}
