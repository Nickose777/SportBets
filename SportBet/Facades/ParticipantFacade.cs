using AutoMapper;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DataServiceMessage<IEnumerable<ParticipantDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<ParticipantDisplayDTO> participantDTOs = serviceMessage.Data;
                    participantModels = participantDTOs.Select(participantDTO => Mapper.Map<ParticipantDisplayDTO, ParticipantDisplayModel>(participantDTO));
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
