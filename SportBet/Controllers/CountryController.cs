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
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;
using AutoMapper;
using SportBet.Contracts.Controllers;
using System.Windows.Controls;

namespace SportBet.Controllers
{
    class CountryController : SubjectBase, ISubject, ICountryController
    {
        private readonly FacadeBase<string> facade;

        public CountryController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Add()
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

        public void Display()
        {
            UserControl control = GetDisplayControl();
            Window window = WindowFactory.CreateByContentsSize(control);

            window.Show();
        }

        public UserControl GetDisplayControl()
        {
            CountryListViewModel viewModel = new CountryListViewModel(this, facade);
            CountryListControl control = new CountryListControl(viewModel);

            viewModel.CountrySelected += (s, e) => EditCountry(e.CountryName);

            return control;
        }

        private void EditCountry(string countryName)
        {
            CountryEditViewModel viewModel = new CountryEditViewModel(countryName);
            CountryEditControl control = new CountryEditControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.CountryEdited += (s, e) =>
            {
                CountryEditModel countryEditModel = e.Country;
                CountryEditDTO countryEditDTO = Mapper.Map<CountryEditModel, CountryEditDTO>(countryEditModel);

                using (ICountryService service = factory.CreateCountryService())
                {
                    ServiceMessage serviceMessage = service.Update(countryEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        window.Close();
                        Notify();
                    }
                }
            };

            window.Show();
        }
    }
}
