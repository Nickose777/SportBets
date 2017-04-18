using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ICountryRepository : IRepository<CountryEntity>
    {
        CountryEntity Get(string countryName);
    }
}
