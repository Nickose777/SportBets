using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class CountryRepository : RepositoryBase<CountryEntity>, ICountryRepository
    {
        public CountryRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
