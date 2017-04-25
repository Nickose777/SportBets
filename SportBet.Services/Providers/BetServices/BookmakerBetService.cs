using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.BetServices
{
    public class BookmakerBetService : IBetService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerBetService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(BetCreateDTO betCreateDTO)
        {
            string message = "";
            bool success = true;

            string sportName = betCreateDTO.SportName;
            string tournamentName = betCreateDTO.TournamentName;
            DateTime dateOfEvent = betCreateDTO.DateOfEvent;
            List<ParticipantBaseDTO> participants = betCreateDTO.EventParticipants;
            string coefficientDescription = betCreateDTO.CoefficientDescription;
            decimal sum = betCreateDTO.Sum;
            string clientPhoneNumber = betCreateDTO.ClientPhoneNumber;
            string bookmakerPhoneNumber = betCreateDTO.BookmakerPhoneNumber;

            if (success = Validate(betCreateDTO, ref message))
            {
                try
                {
                    IEnumerable<ParticipantEntity> participantEntities = participants
                        .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName));

                    EventEntity eventEntity = unitOfWork
                        .Events
                        .Get(sportName, tournamentName, dateOfEvent, participantEntities);

                    CoefficientEntity coefficientEntity = unitOfWork
                        .Coefficients
                        .Get(eventEntity.Id, coefficientDescription);

                    if (coefficientEntity != null)
                    {
                        bool exists = unitOfWork.Bets.Exists(coefficientEntity.Id, clientPhoneNumber);
                        if (!exists)
                        {
                            ClientEntity clientEntity = unitOfWork
                                .Clients
                                .Get(clientPhoneNumber);

                            BookmakerEntity bookmakerEntity = unitOfWork
                                .Bookmakers
                                .Get(bookmakerPhoneNumber);

                            BetEntity betEntity = new BetEntity
                            {
                                ClientId = clientEntity.Id,
                                BookmakerId = bookmakerEntity.Id,
                                CoefficientId = coefficientEntity.Id,
                                RegistrationDate = DateTime.Now,
                                Sum = sum
                            };

                            unitOfWork.Bets.Add(betEntity);
                            unitOfWork.Commit();

                            message = "Created new bet";
                        }
                        else
                        {
                            message = "Such bet already exists";
                            success = true;
                        }
                    }
                    else
                    {
                        message = "Such coefficient was not found";
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(BetCreateDTO betCreateDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(betCreateDTO.BookmakerPhoneNumber))
            {
                message = "Bookmaker's phone number is required";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betCreateDTO.ClientPhoneNumber))
            {
                message = "Client's phone number is required";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betCreateDTO.CoefficientDescription))
            {
                message = "Invalid description of coefficient";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betCreateDTO.SportName))
            {
                message = "Invalid sport name";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betCreateDTO.TournamentName))
            {
                message = "Invalid tournament name";
                isValid = false;
            }
            else if (betCreateDTO.Sum <= 0)
            {
                message = "Invalid sum: must be more then 0";
                isValid = false;
            }

            return isValid;
        }
    }
}
