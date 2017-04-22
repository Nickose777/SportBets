using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.CoefficientServices
{
    public class AdminCoefficientService : ICoefficientService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminCoefficientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

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
                    bool exists = unitOfWork.Coefficients.Exists(eventEntity.Id, value, description);

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

            return isValid;
        }
    }
}
