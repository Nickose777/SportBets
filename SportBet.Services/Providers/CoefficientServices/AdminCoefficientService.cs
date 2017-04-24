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

namespace SportBet.Services.Providers.CoefficientServices
{
    public class AdminCoefficientService : ICoefficientService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminCoefficientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //TODO
        //merge create and update without code duplication
        public ServiceMessage Create(CoefficientCreateDTO coefficientCreateDTO)
        {
            string message = "";
            bool success = true;

            if (success = IsValid(coefficientCreateDTO, ref message))
            {
                string sportName = coefficientCreateDTO.SportName;
                string tournamentName = coefficientCreateDTO.TournamentName;
                DateTime dateOfEvent = coefficientCreateDTO.DateOfEvent;
                List<ParticipantBaseDTO> participants = coefficientCreateDTO.Participants;
                decimal value = coefficientCreateDTO.Value;
                string description = coefficientCreateDTO.Description;

                try
                {
                    IEnumerable<ParticipantEntity> participantEntities = participants
                        .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName))
                        .ToList();

                    EventEntity eventEntity = unitOfWork.Events.Get(sportName, tournamentName, dateOfEvent, participantEntities);
                    bool exists = unitOfWork.Coefficients.Exists(eventEntity.Id, description);

                    if (!exists)
                    {
                        CoefficientEntity coefficientEntity = new CoefficientEntity
                        {
                            EventId = eventEntity.Id,
                            Value = value,
                            Description = description
                        };

                        unitOfWork.Coefficients.Add(coefficientEntity);
                        unitOfWork.Commit();

                        message = "Coefficient added";
                    }
                    else
                    {
                        message = "Such coefficient already exists";
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

        public ServiceMessage Update(CoefficientEditDTO coefficientEditDTO)
        {
            string message = "";
            bool success = true;

            if (success = IsValid(coefficientEditDTO, ref message))
            {
                string sportName = coefficientEditDTO.SportName;
                string tournamentName = coefficientEditDTO.TournamentName;
                DateTime dateOfEvent = coefficientEditDTO.DateOfEvent;
                List<ParticipantBaseDTO> participants = coefficientEditDTO.Participants;
                decimal value = coefficientEditDTO.Value;
                string description = coefficientEditDTO.Description;

                try
                {
                    IEnumerable<ParticipantEntity> participantEntities = participants
                        .Select(p => unitOfWork.Participants.Get(p.Name, p.SportName, p.CountryName))
                        .ToList();

                    EventEntity eventEntity = unitOfWork.Events.Get(sportName, tournamentName, dateOfEvent, participantEntities);
                    bool exists = unitOfWork.Coefficients.Exists(eventEntity.Id, description);

                    if (exists)
                    {
                        CoefficientEntity coefficientEntity = unitOfWork.Coefficients.Get(eventEntity.Id, description);

                        coefficientEntity.Value = coefficientEditDTO.NewValue;
                        coefficientEntity.Description = coefficientEditDTO.NewDescription;

                        unitOfWork.Commit();

                        message = "Coefficient edited";
                    }
                    else
                    {
                        message = "Such coefficient doesn't exist";
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

        public DataServiceMessage<IEnumerable<CoefficientDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<CoefficientDisplayDTO> coefficientDisplayDTOs = null;

            try
            {
                IEnumerable<CoefficientEntity> coefficientEntities = unitOfWork
                    .Coefficients.GetAll();

                coefficientDisplayDTOs = coefficientEntities
                    .Select(coefficientEntity =>
                    {
                        IEnumerable<ParticipantEntity> participantEntities = coefficientEntity
                            .Event
                            .Participations
                            .Select(part => part.Participant);
                        return new CoefficientDisplayDTO
                        {
                            SportName = coefficientEntity.Event.Tournament.Sport.Type,
                            TournamentName = coefficientEntity.Event.Tournament.Name,
                            DateOfEvent = coefficientEntity.Event.DateOfEvent,
                            Description = coefficientEntity.Description,
                            Value = coefficientEntity.Value,
                            Win = coefficientEntity.Win,
                            Participants = participantEntities
                                .Select(p =>
                                {
                                    return new ParticipantBaseDTO
                                    {
                                        SportName = p.Sport.Type,
                                        CountryName = p.Country.Name,
                                        Name = p.Name
                                    };
                                })
                                .ToList()
                        };
                    })
                    .OrderBy(c => c.SportName)
                    .ThenBy(c => c.TournamentName)
                    .ThenBy(c => c.DateOfEvent)
                    .ToList();
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<CoefficientDisplayDTO>>(coefficientDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool IsValid(CoefficientCreateDTO coefficientCreateDTO, ref string message)
        {
            bool isValid = true;

            if (coefficientCreateDTO.Value <= 0)
            {
                message = "Value must be higher than 0";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(coefficientCreateDTO.Description))
            {
                message = "Coefficient must have a description";
                isValid = false;
            }

            return isValid;
        }

        private bool IsValid(CoefficientEditDTO coefficientEditDTO, ref string message)
        {
            bool isValid = true;

            if (coefficientEditDTO.Value <= 0 || coefficientEditDTO.NewValue <= 0)
            {
                message = "Value must be higher than 0";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(coefficientEditDTO.Description))
            {
                message = "Coefficient must have a description";
                isValid = false;
            }

            return isValid;
        }
    }
}
