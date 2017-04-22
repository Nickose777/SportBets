using System.Linq;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using AutoMapper;

namespace SportBet.Facades
{
    class EventFacade : FacadeBase<EventDisplayModel>
    {
        public EventFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<EventDisplayModel> GetAll()
        {
            IEnumerable<EventDisplayModel> eventDisplayModels = null;

            using (IEventService service = factory.CreateEventService())
            {
                DataServiceMessage<IEnumerable<EventDisplayDTO>> serviceMessage = service.GetAll();
                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<EventDisplayDTO> eventDisplayDTOs = serviceMessage.Data;
                    eventDisplayModels = eventDisplayDTOs
                        .Select(eventDisplayDTO => Mapper.Map<EventDisplayDTO, EventDisplayModel>(eventDisplayDTO));
                }
                else
                {
                    eventDisplayModels = new List<EventDisplayModel>();
                }
            }

            return eventDisplayModels;
        }
    }
}
