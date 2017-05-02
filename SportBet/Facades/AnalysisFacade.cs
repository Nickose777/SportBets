using System;
using SportBet.Controllers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Facades
{
    public class AnalysisFacade : ControllerBase
    {
        public AnalysisFacade(ServiceFactory factory)
            : base(factory) { }

        public IncomeDTO GetIncome(DateTime dateStart, DateTime dateEnd)
        {
            IncomeDTO income = null;

            using (IAnalysisService service = factory.CreateAnalysisService())
            {
                DataServiceMessage<IncomeDTO> serviceMessage = service.GetIncome(dateStart, dateEnd);
                RaiseReceivedMessageEvent(serviceMessage);

                income = serviceMessage.IsSuccessful ? serviceMessage.Data : new IncomeDTO();
            }

            return income;
        }
    }
}
