using System;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ICountryService : IDisposable
    {
        ServiceMessage Create(string countryName);
    }
}
