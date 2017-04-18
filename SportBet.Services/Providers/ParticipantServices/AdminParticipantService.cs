using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.ParticipantServices
{
    public class AdminParticipantService : IParticipantService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminParticipantService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(ParticipantCreateDTO participantCreateDTO)
        {
            string message;
            bool success = true;

            string sportName = participantCreateDTO.SportName;
            string countryName = participantCreateDTO.CountryName;
            string participantName = participantCreateDTO.ParticipantName;

            try
            {
                SportEntity sportEntity = unitOfWork.Sports.Get(sportName);
                CountryEntity countryEntity = unitOfWork.Countries.Get(countryName);

                if (sportEntity != null && countryEntity != null)
                {
                    bool exists = unitOfWork.Participants.Exists(participantName, sportName, countryName);
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

        public DataServiceMessage<IEnumerable<ParticipantDisplayDTO>> GetAll()
        {
            string message;
            bool success = true;
            IEnumerable<ParticipantDisplayDTO> partipantDisplayDTOs = null;

            try
            {
                IEnumerable<ParticipantEntity> participantEntities = unitOfWork.Participants.GetAll();

                partipantDisplayDTOs = participantEntities
                    .Select(participant =>
                    {
                        return new ParticipantDisplayDTO
                        {
                            ParticipantName = participant.Name,
                            CountryName = participant.Country.Name,
                            SportName = participant.Sport.Type
                        };
                    })
                    .OrderBy(p => p.ParticipantName)
                    .ToList();

                message = "Got all participants";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ParticipantDisplayDTO>>(partipantDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
