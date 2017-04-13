using System;
using System.Collections.Generic;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ISportService : IDisposable
    {
        ServiceMessage Create(string sportName);

        DataServiceMessage<IEnumerable<string>> GetAll();
    }
}
