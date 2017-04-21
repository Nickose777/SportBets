using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.Contracts.Services
{
    public interface ITournamentService : IDisposable
    {
        ServiceMessage Create(TournamentBaseDTO tournamentCreateDTO);

        ServiceMessage Update(TournamentEditDTO tournamentEditDTO);

        ServiceMessage UpdateParticipants(TournamentEditDTO tournamentEditDTO);

        DataServiceMessage<IEnumerable<TournamentDisplayDTO>> GetAll();
    }
}
