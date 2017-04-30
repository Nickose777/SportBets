using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.Contracts.Services
{
    public interface IEventService : IDisposable
    {
        ServiceMessage CreateWithParticipants(EventCreateDTO eventCreateDTO);

        ServiceMessage Update(EventEditDTO eventEditDTO);

        ServiceMessage UpdateParticipants(EventEditDTO eventEditDTO);

        ServiceMessage Delete(EventBaseDTO eventBaseDTO);

        DataServiceMessage<IEnumerable<EventDisplayDTO>> GetAll();
    }
}
