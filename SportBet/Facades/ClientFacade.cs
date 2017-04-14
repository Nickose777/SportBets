using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Facades
{
    class ClientFacade : FacadeBase<ClientDisplayModel>
    {
        public ClientFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<ClientDisplayModel> GetAll()
        {
            IEnumerable<ClientDisplayModel> clientModels = null;

            using (IClientService service = factory.CreateClientService())
            {
                DataServiceMessage<IEnumerable<ClientDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<ClientDisplayDTO> clientDTOs = serviceMessage.Data;
                    clientModels = clientDTOs.Select(clientDTO => Mapper.Map<ClientDisplayDTO, ClientDisplayModel>(clientDTO));
                }
                else
                {
                    clientModels = new List<ClientDisplayModel>();
                }
            }

            return clientModels;
        }
    }
}
