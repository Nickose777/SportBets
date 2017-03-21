using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class ParticipantRepository : RepositoryBase<ParticipantEntity>, IRepository<ParticipantEntity>
    {
        public ParticipantRepository(SportBetDbContext context)
            : base(context) { }
    }
}
