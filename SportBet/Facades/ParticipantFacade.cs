using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Facades
{
    class ParticipantFacade : FacadeBase<ParticipantDisplayModel>
    {
        public ParticipantFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<ParticipantDisplayModel> GetAll()
        {
            IEnumerable<ParticipantDisplayModel> participantModels = null;

            using (IParticipantService service = factory.CreateParticipantService())
            {
                DataServiceMessage<IEnumerable<ParticipantBaseDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<ParticipantBaseDTO> participantDTOs = serviceMessage.Data;
                    participantModels = participantDTOs.Select(participantDTO => Mapper.Map<ParticipantBaseDTO, ParticipantDisplayModel>(participantDTO));
                }
                else
                {
                    participantModels = new List<ParticipantDisplayModel>();
                }
            }

            return participantModels;
        }
    }
}
