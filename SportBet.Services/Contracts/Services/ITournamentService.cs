using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface ITournamentService : IDisposable
    {
        ServiceMessage Create(TournamentCreateDTO tournamentCreateDTO);

        ServiceMessage Update(TournamentEditDTO tournamentEditDTO);

        DataServiceMessage<IEnumerable<TournamentDisplayDTO>> GetAll();
    }
}
