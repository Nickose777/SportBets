using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ISportService : IDisposable
    {
        ServiceMessage Create(string sportName);

        ServiceMessage Update(SportEditDTO sportEditDTO);

        DataServiceMessage<IEnumerable<string>> GetAll();
    }
}
