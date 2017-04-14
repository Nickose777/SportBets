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
    class AnalyticFacade : FacadeBase<AnalyticDisplayModel>
    {
        public AnalyticFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<AnalyticDisplayModel> GetAll()
        {
            IEnumerable<AnalyticDisplayModel> analyticModels = null;

            using (IAnalyticService service = factory.CreateAnalyticService())
            {
                DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<AnalyticDisplayDTO> analyticDTOs = serviceMessage.Data;
                    analyticModels = analyticDTOs.Select(analyticDTO => Mapper.Map<AnalyticDisplayDTO, AnalyticDisplayModel>(analyticDTO));
                }
                else
                {
                    analyticModels = new List<AnalyticDisplayModel>();
                }
            }

            return analyticModels;
        }
    }
}
