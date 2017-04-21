using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Extra;

namespace SportBet.Services.Providers.ParticipantServices
{
    public class AdminParticipantService : IParticipantService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminParticipantService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(ParticipantBaseDTO participantBaseDTO)
        {
            string message;
            bool success = true;

            string sportName = participantBaseDTO.SportName;
            string countryName = participantBaseDTO.CountryName;
            string participantName = participantBaseDTO.Name;

            try
            {
                SportEntity sportEntity = unitOfWork.Sports.Get(sportName);
                CountryEntity countryEntity = unitOfWork.Countries.Get(countryName);

                if (sportEntity != null && countryEntity != null)
                {
                    bool exists = unitOfWork.Participants.Exists(participantName, sportEntity.Id, countryEntity.Id);
                    if (!exists)
                    {
                        ParticipantEntity participantEntity = new ParticipantEntity
                        {
                            Name = participantName,
                            CountryId = countryEntity.Id,
                            SportId = sportEntity.Id
                        };

                        unitOfWork.Participants.Add(participantEntity);
                        unitOfWork.Commit();

                        message = "Created new participant";
                    }
                    else
                    {
                        message = "Such participant already exists";
                        success = false;
                    }
                }
                else
                {
                    message = "Such sport or country was not found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Update(ParticipantEditDTO participantEditDTO)
        {
            string message;
            bool success = true;

            string oldName = participantEditDTO.OldParticipantName;
            string oldSportName = participantEditDTO.OldSportName;
            string oldCountryName = participantEditDTO.OldCountryName;

            string newName = participantEditDTO.Name;
            string newSportName = participantEditDTO.SportName;
            string newCountryName = participantEditDTO.CountryName;

            try
            {
                ParticipantEntity participantEntity = unitOfWork.Participants.Get(oldName, oldSportName, oldCountryName);
                if (participantEntity != null)
                {
                    SportEntity sportEntity = unitOfWork.Sports.Get(newSportName);
                    CountryEntity countryEntity = unitOfWork.Countries.Get(newCountryName);
                    if (sportEntity != null && countryEntity != null)
                    {
                        participantEntity.Name = newName;
                        participantEntity.SportId = sportEntity.Id;
                        participantEntity.CountryId = countryEntity.Id;

                        unitOfWork.Commit();

                        message = "Edited participant";
                    }
                    else
                    {
                        message = "Such sport or country was not found";
                        success = false;
                    }
                }
                else
                {
                    message = "Such participant was not found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetBySport(string sportName)
        {
            string message;
            bool success = true;
            IEnumerable<ParticipantBaseDTO> partipantDisplayDTOs = null;

            try
            {
                SportEntity sportEntity = unitOfWork.Sports.Get(sportName);
                if (sportEntity != null)
                {
                    IEnumerable<ParticipantEntity> participantEntities = unitOfWork.Participants.GetAll(p => p.SportId == sportEntity.Id);

                    partipantDisplayDTOs = participantEntities
                        .Select(participant =>
                        {
                            return new ParticipantBaseDTO
                            {
                                Name = participant.Name,
                                CountryName = participant.Country.Name,
                                SportName = participant.Sport.Type
                            };
                        })
                        .OrderBy(p => p.Name)
                        .ToList();

                    message = "Got all participants";
                }
                else
                {
                    message = "No such sport found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ParticipantBaseDTO>>(partipantDisplayDTOs, message, success);
        }

        public DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetByTournament(TournamentBaseDTO tournamentBaseDTO)
        {
            string message;
            bool success = true;
            IEnumerable<ParticipantBaseDTO> partipantDisplayDTOs = null;

            string name = tournamentBaseDTO.Name;
            string sportName = tournamentBaseDTO.SportName;
            DateTime dateOfStart = tournamentBaseDTO.DateOfStart;

            try
            {
                TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(name, sportName, dateOfStart);
                if (tournamentEntity != null)
                {
                    IEnumerable<ParticipantEntity> participantEntities = tournamentEntity.Participants;

                    partipantDisplayDTOs = participantEntities
                        .Select(participant =>
                        {
                            return new ParticipantBaseDTO
                            {
                                Name = participant.Name,
                                CountryName = participant.Country.Name,
                                SportName = participant.Sport.Type
                            };
                        })
                        .OrderBy(p => p.Name)
                        .ToList();

                    message = "Got all participants";
                }
                else
                {
                    message = "No such tournament found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ParticipantBaseDTO>>(partipantDisplayDTOs, message, success);
        }

        public DataServiceMessage<IEnumerable<ParticipantBaseDTO>> GetAll()
        {
            string message;
            bool success = true;
            IEnumerable<ParticipantBaseDTO> partipantDisplayDTOs = null;

            try
            {
                IEnumerable<ParticipantEntity> participantEntities = unitOfWork.Participants.GetAll();

                partipantDisplayDTOs = participantEntities
                    .Select(participant =>
                    {
                        return new ParticipantBaseDTO
                        {
                            Name = participant.Name,
                            CountryName = participant.Country.Name,
                            SportName = participant.Sport.Type
                        };
                    })
                    .OrderBy(p => p.Name)
                    .ToList();

                message = "Got all participants";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ParticipantBaseDTO>>(partipantDisplayDTOs, message, success);
        }

        public DataServiceMessage<IEnumerable<ParticipantTournamentDTO>> GetAllWithTournaments()
        {
            string message;
            bool success = true;
            IEnumerable<ParticipantTournamentDTO> partipantDTOs = null;

            try
            {
                IEnumerable<ParticipantEntity> participantEntities = unitOfWork.Participants.GetAll();

                partipantDTOs = participantEntities
                    .Select(participant =>
                    {
                        IEnumerable<TournamentEntity> tournaments = unitOfWork
                            .Tournaments
                            .GetAll(t => t.Participants.Select(p => p.Id).Contains(participant.Id));
                        return new ParticipantTournamentDTO
                        {
                            Name = participant.Name,
                            CountryName = participant.Country.Name,
                            SportName = participant.Sport.Type,
                            Tournaments = tournaments
                                .Select(t =>
                                {
                                    return new TournamentBaseDTO
                                    {
                                        Name = t.Name,
                                        DateOfStart = t.DateOfStart,
                                        SportName = t.Sport.Type
                                    };
                                })
                                .ToList()
                        };
                    })
                    .OrderBy(p => p.Name)
                    .ToList();

                message = "Got all participants";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ParticipantTournamentDTO>>(partipantDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
