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
    class CountryController : SubjectBase, ISubject
    {
        private readonly FacadeBase<string> facade;

        public CountryController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void AddCountry()
        {
            CountryCreateViewModel viewModel = new CountryCreateViewModel();
            CountryCreateControl control = new CountryCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.CountryCreated += (s, e) =>
            {
                using (ICountryService service = factory.CreateCountryService())
                {
                    ServiceMessage serviceMessage = service.Create(e.ContryName);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.CountryName = String.Empty;
                        Notify();
                    }
                }
            };

            window.Show();
        }

        public void DisplayCountries()
        {
            CountryListViewModel viewModel = new CountryListViewModel(this, facade);
            CountryListControl control = new CountryListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.Show();
        }
    }
}
