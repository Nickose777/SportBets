using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IParticipantService : IDisposable
    {
        ServiceMessage Create(ParticipantCreateDTO participantCreateDTO);

        DataServiceMessage<IEnumerable<ParticipantDisplayDTO>> GetAll();
    }
}
