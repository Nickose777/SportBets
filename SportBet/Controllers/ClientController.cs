using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportBet.Contracts.Controllers;
using SportBet.Models.Display;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Controllers
{
    class ClientController : BaseController, IClientController
    {
        private readonly ServiceFactory factory;

        public ClientController(ServiceFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<ClientDisplayModel> GetAllNotDeleted()
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
