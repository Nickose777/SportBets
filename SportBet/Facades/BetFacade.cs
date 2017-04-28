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
    class BetFacade : FacadeBase<BetDisplayModel>
    {
        public BetFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<BetDisplayModel> GetAll()
        {
            IEnumerable<BetDisplayModel> betModels = null;

            using (IBetService service = factory.CreateBetService())
            {
                DataServiceMessage<IEnumerable<BetDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<BetDisplayDTO> betDTOs = serviceMessage.Data;
                    betModels = betDTOs.Select(betDTO => Mapper.Map<BetDisplayDTO, BetDisplayModel>(betDTO));
                }
                else
                {
                    betModels = new List<BetDisplayModel>();
                }
            }

            return betModels;
        }
    }
}
