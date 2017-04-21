using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.Contracts.Services
{
    public interface IParticipantService : IDisposable
    {
        ServiceMessage Create(ParticipantBaseDTO participantBaseDTO);

        ServiceMessage Update(ParticipantEditDTO participantEditDTO);

        DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetBySport(string sportName);

        DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetByTournament(TournamentBaseDTO tournamentBaseDTO);

        DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetAll();
    }
}
