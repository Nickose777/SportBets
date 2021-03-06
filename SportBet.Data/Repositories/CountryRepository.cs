﻿using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class CountryRepository : RepositoryBase<CountryEntity>, ICountryRepository
    {
        public CountryRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public bool Exists(string countryName)
        {
            CountryEntity countryEntity = Get(countryName);

            return countryEntity != null;
        }

        public CountryEntity Get(string countryName)
        {
            SportBetDbContext context = GetContext();
            CountryEntity countryEntity = context.Countries.SingleOrDefault(country => country.Name == countryName);

            return countryEntity;
        }
    }
}
