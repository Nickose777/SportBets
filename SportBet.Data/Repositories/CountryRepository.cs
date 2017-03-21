using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class CountryRepository : RepositoryBase<CountryEntity>, IRepository<CountryEntity>
    {
        public CountryRepository(SportBetDbContext context)
            : base(context) { }
    }
}
