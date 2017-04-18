using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
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

            //TODO
            //using (IParticipantService service = factory.CreateParticipantService())
            //{
            //    DataServiceMessage<IEnumerable<ParticipantDisplayDTO>> serviceMessage = service.GetAll();

            //    RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
            //    if (serviceMessage.IsSuccessful)
            //    {
            //        IEnumerable<AdminDisplayDTO> adminDTOs = serviceMessage.Data;
            //        adminModels = adminDTOs.Select(adminDTO => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(adminDTO));
            //    }
            //    else
            //    {
            //        adminModels = new List<AdminDisplayModel>();
            //    }
            //}

            return participantModels;
        }
    }
}
