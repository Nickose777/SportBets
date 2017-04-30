using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ICountryService : IDisposable
    {
        ServiceMessage Create(string countryName);

        ServiceMessage Update(CountryEditDTO countryEditDTO);

        ServiceMessage Delete(string countryName);

        DataServiceMessage<IEnumerable<string>> GetAll();
    }
}
