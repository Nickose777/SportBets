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

namespace SportBet.Services.Providers.EventServices
{
    class BookmakerEventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerEventService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage CreateWithParticipants(EventCreateDTO eventCreateDTO)
        {
            return new ServiceMessage("No permisssions", false);
        }

        public ServiceMessage Update(EventEditDTO eventEditDTO)
        {
            return new ServiceMessage("No permisssions", false);
        }

        public ServiceMessage UpdateParticipants(EventEditDTO eventEditDTO)
        {
            return new ServiceMessage("No permisssions", false);
        }

        public ServiceMessage Delete(EventBaseDTO eventBaseDTO)
        {
            return new ServiceMessage("No permisssions", false);
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
    }
}
