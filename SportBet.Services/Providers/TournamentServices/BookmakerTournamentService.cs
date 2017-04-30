using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.TournamentServices
{
    class BookmakerTournamentService : ITournamentService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerTournamentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(TournamentBaseDTO tournamentCreateDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Update(TournamentEditDTO tournamentEditDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage UpdateParticipants(TournamentEditDTO tournamentEditDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Delete(TournamentBaseDTO tournamentBaseDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public DataServiceMessage<IEnumerable<TournamentDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<TournamentDisplayDTO> tournamentDisplayDTOs = null;

            try
            {
                IEnumerable<TournamentEntity> tournamentEntities = unitOfWork.Tournaments.GetAll();
                tournamentDisplayDTOs = tournamentEntities
                    .Select(tournamentEntity =>
                    {
                        return new TournamentDisplayDTO
                        {
                            Name = tournamentEntity.Name,
                            SportName = tournamentEntity.Sport.Type,
                            DateOfStart = tournamentEntity.DateOfStart
                        };
                    })
                    .OrderBy(t => t.Name)
                    .ToList();

                message = "Got all tournaments";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<TournamentDisplayDTO>>(tournamentDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
