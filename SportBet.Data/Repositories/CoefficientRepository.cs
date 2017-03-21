using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class CoefficientRepository : RepositoryBase<CoefficientEntity>, IRepository<CoefficientEntity>
    {
        public CoefficientRepository(SportBetDbContext context)
            : base(context) { }

        public override CoefficientEntity Get(int id)
        {
            throw new InvalidOperationException();
        }
    }
}
