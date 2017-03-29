using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class BetRepository : RepositoryBase<BetEntity>, IBetRepository
    {
        public BetRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public override BetEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
