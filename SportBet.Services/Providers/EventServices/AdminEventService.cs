using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
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

        //TODO rewrite
        public ServiceMessage CreateWithParticipants(EventCreateDTO eventCreateDTO)
        {
            string message = "";
            bool success = true;

            string sportName = eventCreateDTO.SportName;
            string tournamentName = eventCreateDTO.TournamentName;
            DateTime dateOfTournamentStart = eventCreateDTO.DateOfTournamentStart;

            string notes = eventCreateDTO.Notes;
            DateTime dateOfEvent = eventCreateDTO.DateOfEvent;
            List<ParticipantBaseDTO> participants = eventCreateDTO.Participants;

            if (participants != null && participants.Count > 1)
            {
                if (success = ValidateDate(dateOfEvent, ref message))
                {
                    try
                    {
                        TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(tournamentName, sportName, dateOfTournamentStart);
                        if (tournamentEntity != null)
                        {
                            if (dateOfEvent.Date >= dateOfTournamentStart.Date)
                            {
                                bool isDualSport = tournamentEntity.Sport.IsDual;
                                if (isDualSport)
                                {
                                    if (participants.Count == 2)
                                    {
                                        IEnumerable<ParticipantEntity> participantEntities = participants
                                            .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName));

                                        bool isAnyParticipantBusy = participantEntities.Any(p => unitOfWork.Participants.IsBusyOn(p.Id, dateOfEvent));

                                        if (!isAnyParticipantBusy)
                                        {
                                            EventEntity eventEntity = new EventEntity
                                            {
                                                DateOfEvent = dateOfEvent,
                                                Notes = notes,
                                                TournamentId = tournamentEntity.Id
                                            };

                                            IEnumerable<ParticipationEntity> participations = participantEntities
                                                .Select(p =>
                                                {
                                                    return new ParticipationEntity
                                                    {
                                                        Event = eventEntity,
                                                        Participant = p
                                                    };
                                                });

                                            foreach (var participation in participations)
                                            {
                                                unitOfWork.Participations.Add(participation);
                                            }
                                            unitOfWork.Commit();

                                            message = String.Format("Created new event with {0} participants", participants.Count);
                                        }
                                        else
                                        {
                                            message = "Some participants cannot play on this date";
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        message = "Sport is dual. Only two participants can take part";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    IEnumerable<ParticipantEntity> participantEntities = participants
                                        .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName));

                                    bool isAnyParticipantBusy = participantEntities.Any(p => unitOfWork.Participants.IsBusyOn(p.Id, dateOfEvent));

                                    if (!isAnyParticipantBusy)
                                    {
                                        EventEntity eventEntity = new EventEntity
                                        {
                                            DateOfEvent = dateOfEvent,
                                            Notes = notes,
                                            TournamentId = tournamentEntity.Id
                                        };

                                        IEnumerable<ParticipationEntity> participations = participantEntities
                                            .Select(p =>
                                            {
                                                return new ParticipationEntity
                                                {
                                                    Event = eventEntity,
                                                    Participant = p
                                                };
                                            });

                                        foreach (var participation in participations)
                                        {
                                            unitOfWork.Participations.Add(participation);
                                        }
                                        unitOfWork.Commit();

                                        message = String.Format("Created new event with {0} participants", participants.Count);
                                    }
                                    else
                                    {
                                        message = "Some participants cannot play on this date";
                                        success = false;
                                    }
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
            }
            else
            {
                message = "Invalid count of participants";
                success = false;
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
