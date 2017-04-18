using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IParticipantService : IDisposable
    {
        ServiceMessage Create(ParticipantCreateDTO participantCreateDTO);
    }
}
