using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.CoefficientServices
{
    class BookmakerCoefficientService : ICoefficientService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerCoefficientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(CoefficientCreateDTO coefficientCreateDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Update(CoefficientEditDTO coefficientEditDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Delete(CoefficientBaseDTO coefficientBaseDTO)
        {
            return new ServiceMessage("No permissions", false);
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
    }
}
