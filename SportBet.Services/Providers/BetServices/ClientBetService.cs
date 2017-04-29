using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportBet.Services.Providers.BetServices
{
    public class ClientBetService : IBetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISession session;

        public ClientBetService(IUnitOfWork unitOfWork, ISession session)
        {
            this.unitOfWork = unitOfWork;
            this.session = session;
        }

        public ServiceMessage Create(BetCreateDTO betCreateDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Update(BetEditDTO betEditDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public DataServiceMessage<IEnumerable<BetDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<BetDisplayDTO> betDisplayDTOs = null;

            try
            {
                string login = session.CurrentUserLogin;
                int id = unitOfWork.Users.GetIdByLogin(login);

                IEnumerable<BetEntity> betEntities = unitOfWork.Bets.GetAll(b => b.ClientId == id);
                if (betEntities.Count() != 0)
                {
                    betDisplayDTOs = betEntities
                        .Select(betEntity =>
                        {
                            List<ParticipantBaseDTO> participants = betEntity
                                .Coefficient
                                .Event
                                .Participations
                                .Select(part => part.Participant)
                                .Select(participantEntity =>
                                {
                                    return new ParticipantBaseDTO
                                    {
                                        CountryName = participantEntity.Country.Name,
                                        Name = participantEntity.Name,
                                        SportName = participantEntity.Sport.Type
                                    };
                                })
                                .OrderBy(p => p.Name)
                                .ToList();

                            decimal winSum = 0;

                            bool? win = betEntity.Coefficient.Win;
                            if (win.HasValue && win.Value)
                            {
                                winSum = betEntity.Coefficient.Value * betEntity.Sum;
                            }

                            return new BetDisplayDTO
                            {
                                ClientPhoneNumber = betEntity.Client.PhoneNumber,
                                CoefficientDescription = betEntity.Coefficient.Description,
                                DateOfEvent = betEntity.Coefficient.Event.DateOfEvent,
                                EventParticipants = participants,
                                PossibleWinSum = betEntity.Coefficient.Value * betEntity.Sum,
                                RegistrationDate = betEntity.RegistrationDate,
                                SportName = betEntity.Coefficient.Event.Tournament.Sport.Type,
                                Sum = betEntity.Sum,
                                TournamentName = betEntity.Coefficient.Event.Tournament.Name,
                                Win = betEntity.Coefficient.Win,
                                WinSum = winSum
                            };
                        })
                        .OrderBy(b => b.DateOfEvent)
                        .ThenBy(b => b.TournamentName)
                        .ToList();

                    message = "Got all bets";
                }
                else
                {
                    message = "No bet found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<BetDisplayDTO>>(betDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
