using System.Windows;
using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts;
using SportBet.WindowFactories;

namespace SportBet.Controllers
{
    class CountryController : SubjectBase, ISubject
    {
        private readonly FacadeBase<string> facade;

        public CountryController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void DisplayCountries()
        {
            CountryListViewModel viewModel = new CountryListViewModel(this, facade);
            CountryListControl control = new CountryListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.ShowDialog();
        }
    }
}
