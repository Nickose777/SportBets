using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts.Factories;
using SportBet.WindowFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportBet.Subjects
{
    class CountryDisplayManager : SubjectBase, ISubject
    {
        private readonly ICountryController controller;

        public CountryDisplayManager(ServiceFactory factory, ICountryController controller)
            : base(factory)
        {
            this.controller = controller;
            controller.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void DisplayCountries()
        {
            CountryListViewModel viewModel = new CountryListViewModel(this, controller);
            CountryListControl control = new CountryListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.ShowDialog();
        }
    }
}
