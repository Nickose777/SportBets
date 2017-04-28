using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;

namespace SportBet.Services.Contracts.Services
{
    public interface IBetService : IDisposable
    {
        ServiceMessage Create(BetCreateDTO betCreateDTO);

        DataServiceMessage<IEnumerable<BetDisplayDTO>> GetAll();
    }
}
