using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ICountryRepository : IRepository<CountryEntity>
    {
        bool Exists(string countryName);

        CountryEntity Get(string countryName);
    }
}
