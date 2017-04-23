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


            if (eventCreateDTO.DateOfEvent >= DateTime.Now)
            {
                if (participants != null && participants.Count > 1)
                {
                    if (success = Validate(eventCreateDTO, ref message))
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
            }
            else
            {
                message = "Invalid date: cannot start event in the past";
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Update(EventEditDTO eventEditDTO)
        {
            string message = "";
            bool success = true;

            string sportName = eventEditDTO.SportName;
            string tournamentName = eventEditDTO.TournamentName;
            DateTime dateOfTournamentStart = eventEditDTO.DateOfTournamentStart;

            string notes = eventEditDTO.Notes;
            DateTime dateOfEvent = eventEditDTO.DateOfEvent;

            List<ParticipantBaseDTO> participants = eventEditDTO.Participants;

            if (success = Validate(eventEditDTO, ref message))
            {
                try
                {
                    TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(tournamentName, sportName, dateOfTournamentStart);
                    if (tournamentEntity != null)
                    {
                        IEnumerable<ParticipantEntity> participantEntities = participants
                            .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName))
                            .ToList();

                        EventEntity eventEntity = unitOfWork
                            .Events
                            .Get(sportName, tournamentName, dateOfEvent, participantEntities);
                        if (eventEntity != null)
                        {
                            eventEntity.DateOfEvent = eventEditDTO.NewDateOfEvent;
                            eventEntity.Notes = eventEditDTO.Notes;

                            unitOfWork.Commit();

                            message = "Edited event";
                        }
                        else
                        {
                            message = "Such event was not found";
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

        public ServiceMessage UpdateParticipants(EventEditDTO eventEditDTO)
        {
            string message = "";
            bool success = true;

            string sportName = eventEditDTO.SportName;
            string tournamentName = eventEditDTO.TournamentName;
            DateTime dateOfTournamentStart = eventEditDTO.DateOfTournamentStart;
            DateTime dateOfEvent = eventEditDTO.DateOfEvent;
            List<ParticipantBaseDTO> participants = eventEditDTO.Participants;
            List<ParticipantBaseDTO> newParticipants = eventEditDTO.NewParticipants;

            if (success = Validate(eventEditDTO, ref message))
            {
                if (newParticipants != null && newParticipants.Count > 1)
                {
                    try
                    {
                        TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(tournamentName, sportName, dateOfTournamentStart);
                        if (tournamentEntity != null)
                        {
                            IEnumerable<ParticipantEntity> oldParticipantEntities = participants
                                .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName))
                                .ToList();

                            EventEntity eventEntity = unitOfWork
                                .Events
                                .Get(sportName, tournamentName, dateOfEvent, oldParticipantEntities);
                            if (eventEntity != null)
                            {
                                IEnumerable<ParticipantEntity> newParticipantEntities = newParticipants
                                    .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName));

                                bool sportValidated = true;

                                SportEntity sportEntity = tournamentEntity.Sport;
                                if (sportEntity.IsDual)
                                {
                                    if (newParticipantEntities.Count() != 2)
                                    {
                                        message = "Sport is dual. Only 2 participants allowed";
                                        success = false;
                                        sportValidated = false;
                                    }
                                }

                                if (sportValidated)
                                {
                                    var added = newParticipantEntities.Except(oldParticipantEntities).ToList();
                                    var deleted = oldParticipantEntities.Except(newParticipantEntities).ToList();

                                    bool anyParticipantBusy = added
                                        .Any(p => unitOfWork.Participants.IsBusyOn(p.Id, dateOfEvent));
                                    if (!anyParticipantBusy)
                                    {
                                        var deletedParticipations = eventEntity
                                            .Participations
                                            .Where(part => deleted.Select(p => p.Id).Contains(part.ParticipantId))
                                            .ToList();

                                        foreach (var participation in deletedParticipations)
                                        {
                                            eventEntity.Participations.Remove(participation);
                                        }
                                        foreach (var participant in added)
                                        {
                                            eventEntity.Participations.Add(new ParticipationEntity
                                            {
                                                EventId = eventEntity.Id,
                                                ParticipantId = participant.Id
                                            });
                                        }

                                        unitOfWork.Commit();

                                        message = "Changed participants of event";
                                    }
                                    else
                                    {
                                        message = "One or more participant is busy this date";
                                        success = false;
                                    }
                                }
                            }
                            else
                            {
                                message = "Such event was not found";
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
                else
                {
                    message = "Invalid count of participants";
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
        }

        public DataServiceMessage<IEnumerable<EventDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<EventDisplayDTO> eventDisplayDTOs = null;

            try
            {
                IEnumerable<EventEntity> eventEntities = unitOfWork.Events.GetAll();
                eventDisplayDTOs = eventEntities
                    .Select(eventEntity =>
                    {
                        IEnumerable<ParticipantEntity> participantEntities = unitOfWork
                            .Participants
                            .GetByEvent(eventEntity.Id);

                        return new EventDisplayDTO
                        {
                            DateOfEvent = eventEntity.DateOfEvent,
                            DateOfTournamentStart = eventEntity.Tournament.DateOfStart,
                            Notes = eventEntity.Notes,
                            SportName = eventEntity.Tournament.Sport.Type,
                            TournamentName = eventEntity.Tournament.Name,
                            Participants = participantEntities
                                .Select(participantEntity =>
                                {
                                    return new ParticipantBaseDTO
                                    {
                                        CountryName = participantEntity.Country.Name,
                                        Name = participantEntity.Name,
                                        SportName = participantEntity.Sport.Type
                                    };
                                })
                                .ToList()
                        };
                    })
                    .OrderBy(e => e.DateOfEvent)
                    .ToList();

                message = "Got all events";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<EventDisplayDTO>>(eventDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(EventBaseDTO eventBaseDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(eventBaseDTO.SportName))
            {
                message = "Sport name cannot be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(eventBaseDTO.TournamentName))
            {
                message = "Tournament name cannot be empty";
                isValid = false;
            }
            else if (eventBaseDTO.DateOfEvent < eventBaseDTO.DateOfTournamentStart)
            {
                message = "Invalid date: cannot start event earlier then tournament";
                isValid = false;
            }
            else if (eventBaseDTO.Participants == null)
            {
                message = "Invalid participants";
                isValid = false;
            }
            else if (eventBaseDTO.Participants.Count < 2)
            {
                message = "Invalid count of participants";
                isValid = false;
            }

            return isValid;
        }
    }
}
