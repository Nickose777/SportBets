using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface IBetService : IDisposable
    {
        ServiceMessage Create(BetCreateDTO betCreateDTO);

        ServiceMessage Update(BetEditDTO betEditDTO);

        DataServiceMessage<IEnumerable<BetDisplayDTO>> GetAll();
    }
}
