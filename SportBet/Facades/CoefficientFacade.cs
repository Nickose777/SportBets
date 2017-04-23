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
    class CoefficientFacade : FacadeBase<CoefficientDisplayModel>
    {
        public CoefficientFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<CoefficientDisplayModel> GetAll()
        {
            IEnumerable<CoefficientDisplayModel> coefficientModels = null;

            using (ICoefficientService service = factory.CreateCoefficientService())
            {
                DataServiceMessage<IEnumerable<CoefficientDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<CoefficientDisplayDTO> coefficientDTOs = serviceMessage.Data;
                    coefficientModels = coefficientDTOs.Select(coefficientDTO => Mapper.Map<CoefficientDisplayDTO, CoefficientDisplayModel>(coefficientDTO));
                }
                else
                {
                    coefficientModels = new List<CoefficientDisplayModel>();
                }
            }

            return coefficientModels;
        }
    }
}
