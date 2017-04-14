using System.Windows;
using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts;
using SportBet.WindowFactories;
using SportBet.Contracts;

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

        public void DisplaySports()
        {
            SportListViewModel viewModel = new SportListViewModel(this, facade);
            SportListControl control = new SportListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.ShowDialog();
        }
    }
}
