using System.Windows;
using SportBet.AnalyticControls.UserControls;
using SportBet.AnalyticControls.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.CommonControls.Errors;

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
        
        public UIElement GetSportRatingElement()
        {
            UIElement element = null;

            using (IAnalysisService service = factory.CreateAnalysisService())
            {
                DataServiceMessage<IEnumerable<SportRatingDTO>> serviceMessage = service.GetSportRating();
                RaiseReceivedMessageEvent(serviceMessage);

                if (serviceMessage.IsSuccessful)
                {
                    SportRatingViewModel viewModel = new SportRatingViewModel(serviceMessage.Data);
                    SportRatingControl control = new SportRatingControl(viewModel);

                    element = control;
                }
                else
                {
                    List<ServiceMessage> messages = new List<ServiceMessage>()
                    {
                        serviceMessage
                    };

                    ErrorViewModel viewModel = new ErrorViewModel(messages);
                    ErrorControl control = new ErrorControl(viewModel);

                    element = control;
                }
            }

            return element;
        }

        public UIElement GetBookmakerAnalysisElement()
        {
            UIElement element = null;

            using (IAnalysisService service = factory.CreateAnalysisService())
            {
                DataServiceMessage<IEnumerable<BookmakerAnalysisDTO>> serviceMessage = service.GetBookmakerAnalysis();
                RaiseReceivedMessageEvent(serviceMessage);

                if (serviceMessage.IsSuccessful)
                {
                    BookmakerAnalysisViewModel viewModel = new BookmakerAnalysisViewModel(serviceMessage.Data);
                    BookmakerAnalysisControl control = new BookmakerAnalysisControl(viewModel);

                    element = control;
                }
                else
                {
                    List<ServiceMessage> messages = new List<ServiceMessage>()
                    {
                        serviceMessage
                    };

                    ErrorViewModel viewModel = new ErrorViewModel(messages);
                    ErrorControl control = new ErrorControl(viewModel);

                    element = control;
                }
            }

            return element;
        }

        public UIElement GetClientAnalysisElement()
        {
            throw new System.NotImplementedException();
        }
    }
}
