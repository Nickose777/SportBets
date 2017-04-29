using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

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

            if (success = ValidateBase(betCreateDTO, ref message) && Validate(betCreateDTO, ref message))
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

        public ServiceMessage Update(BetEditDTO betEditDTO)
        {
            string message = "";
            bool success = true;

            string sportName = betEditDTO.SportName;
            string tournamentName = betEditDTO.TournamentName;
            DateTime dateOfEvent = betEditDTO.DateOfEvent;
            List<ParticipantBaseDTO> participants = betEditDTO.EventParticipants;
            string coefficientDescription = betEditDTO.CoefficientDescription;
            decimal sum = betEditDTO.Sum;
            string clientPhoneNumber = betEditDTO.ClientPhoneNumber;

            if (success = ValidateBase(betEditDTO, ref message) && Validate(betEditDTO, ref message))
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
                        BetEntity betEntity = unitOfWork.Bets.Get(coefficientEntity.Id, clientPhoneNumber);
                        if (betEntity != null)
                        {
                            betEntity.Sum = sum;

                            unitOfWork.Commit();

                            message = "Edited bet";
                        }
                        else
                        {
                            message = "Such bet not found";
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

        public DataServiceMessage<IEnumerable<BetDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<BetDisplayDTO> betDisplayDTOs = null;

            try
            {
                IEnumerable<BetEntity> betEntities = unitOfWork.Bets.GetAll();
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

        private bool ValidateBase(BetBaseDTO betBaseDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(betBaseDTO.ClientPhoneNumber))
            {
                message = "Client's phone number is required";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betBaseDTO.CoefficientDescription))
            {
                message = "Invalid description of coefficient";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betBaseDTO.SportName))
            {
                message = "Invalid sport name";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(betBaseDTO.TournamentName))
            {
                message = "Invalid tournament name";
                isValid = false;
            }

            return isValid;
        }
        
        private bool Validate(BetCreateDTO betCreateDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(betCreateDTO.BookmakerPhoneNumber))
            {
                message = "Bookmaker's phone number is required";
                isValid = false;
            }
            else if (betCreateDTO.Sum <= 0)
            {
                message = "Invalid sum: must be more then 0";
                isValid = false;
            }

            return isValid;
        }

        private bool Validate(BetEditDTO betEditDTO, ref string message)
        {
            bool isValid = true;

            if (betEditDTO.Sum <= 0)
            {
                message = "Invalid sum: must be more then 0";
                isValid = false;
            }

            return isValid;
        }
    }
}
