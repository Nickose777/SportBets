using System;
using System.Windows;
using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;

namespace SportBet.Controllers
{
    class SportController : SubjectBase, ISubject
    {
        private readonly FacadeBase<string> facade;

        public SportController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void AddSport()
        {
            SportCreateViewModel viewModel = new SportCreateViewModel();
            SportCreateControl control = new SportCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportCreated += (s, e) =>
            {
                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Create(e.SportName);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.SportName = String.Empty;
                        Notify();
                    }
                }
            };

            window.Show();
        }

        public void DisplaySports()
        {
            SportListViewModel viewModel = new SportListViewModel(this, facade);
            SportListControl control = new SportListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.Show();
        }
    }
}
