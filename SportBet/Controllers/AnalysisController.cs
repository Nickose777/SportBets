using System.Windows;
using SportBet.AnalyticControls.UserControls;
using SportBet.AnalyticControls.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    class AnalysisController : ControllerBase, IAnalysisController
    {
        private readonly AnalysisFacade facade;

        public AnalysisController(ServiceFactory factory, AnalysisFacade facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public UIElement GetIncomeElement()
        {
            IncomeViewModel viewModel = new IncomeViewModel(facade);
            IncomeControl control = new IncomeControl(viewModel);

            return control;
        }
    }
}
