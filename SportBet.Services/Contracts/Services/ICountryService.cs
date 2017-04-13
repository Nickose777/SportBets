using System;
using System.Collections.Generic;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ICountryService : IDisposable
    {
        ServiceMessage Create(string countryName);

        DataServiceMessage<IEnumerable<string>> GetAll();
    }
}
