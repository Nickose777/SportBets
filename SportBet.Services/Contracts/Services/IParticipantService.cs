using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface IParticipantService : IDisposable
    {
        ServiceMessage Create(ParticipantCreateDTO participantCreateDTO);

        ServiceMessage Update(ParticipantEditDTO participantEditDTO);

        DataServiceMessage<IEnumerable<ParticipantDisplayDTO>> GetAll();
    }
}
