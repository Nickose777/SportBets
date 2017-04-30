using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Create;

namespace SportBet.Services.Contracts.Services
{
    public interface ISportService : IDisposable
    {
        ServiceMessage Create(SportCreateDTO sportCreateDTO);

        ServiceMessage Update(SportEditDTO sportEditDTO);

        ServiceMessage Delete(string sportName);

        DataServiceMessage<IEnumerable<string>> GetAll();
    }
}
