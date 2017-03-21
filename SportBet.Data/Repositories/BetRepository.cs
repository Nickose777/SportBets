using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class BetRepository : RepositoryBase<BetEntity>, IRepository<BetEntity>
    {
        public BetRepository(SportBetDbContext context)
            : base(context) { }

        public override BetEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
