using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.EventServices
{
    public class AdminEventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminEventService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(EventCreateDTO eventCreateDTO)
        {
            string message = "";
            bool success = true;
            
            string sportName = eventCreateDTO.SportName;
            string tournamentName = eventCreateDTO.TournamentName;
            DateTime dateOfTournamentStart = eventCreateDTO.DateOfTournamentStart;

            string notes = eventCreateDTO.Notes;
            DateTime dateOfEvent = eventCreateDTO.DateOfEvent;

            if (success = ValidateDate(dateOfEvent, ref message))
            {
                try
                {
                    TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(tournamentName, sportName, dateOfTournamentStart);
                    if (tournamentEntity != null)
                    {
                        if (dateOfEvent.Date >= dateOfTournamentStart.Date)
                        {
                            bool exists = unitOfWork.Events.Exists(dateOfEvent, tournamentEntity.Id);
                            if (!exists)
                            {
                                EventEntity eventEntity = new EventEntity
                                {
                                    DateOfEvent = dateOfEvent,
                                    Notes = notes,
                                    TournamentId = tournamentEntity.Id
                                };

                                unitOfWork.Events.Add(eventEntity);
                                unitOfWork.Commit();

                                message = "Created new event";
                            }
                            else
                            {
                                message = "Such event already exists. Change the date";
                                success = false;
                            }
                        }
                        else
                        {
                            message = "An event cannot start earlier then tournament";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "Such tournament was not found";
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

        private bool ValidateDate(DateTime dateTime, ref string message)
        {
            bool isValid = true;

            if (dateTime < DateTime.Now)
            {
                message = "Invalid date: cannot start event in the past";
                isValid = false;
            }

            return isValid;
        }
    }
}
