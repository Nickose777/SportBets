using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;

namespace SportBet.Services.Contracts.Services
{
    public interface IEventService : IDisposable
    {
        ServiceMessage CreateWithParticipants(EventCreateDTO eventCreateDTO);

        DataServiceMessage<IEnumerable<EventDisplayDTO>> GetAll();
    }
}
