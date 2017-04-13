using System.Windows;
using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts.Factories;
using SportBet.WindowFactories;

namespace SportBet.Subjects
{
    class SportDisplayManager : SubjectBase, ISubject
    {
        private readonly ISportController controller;

        public SportDisplayManager(ServiceFactory factory, ISportController controller)
            : base(factory)
        {
            this.controller = controller;
            controller.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void DisplaySports()
        {
            SportListViewModel viewModel = new SportListViewModel(this, controller);
            SportListControl control = new SportListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.ShowDialog();
        }
    }
}
