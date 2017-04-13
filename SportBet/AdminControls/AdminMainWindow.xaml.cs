using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;
using SportBet.AdminControls.ViewModels;
using SportBet.AdminControls.UserControls;
using SportBet.WindowFactories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using SportBet.Subjects;
using SportBet.Controllers;

namespace SportBet.AdminControls
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MainWindowBase
    {
        private readonly CountryDisplayManager countryDisplayManager;
        private readonly SportDisplayManager sportDisplayManager;

        public AdminMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            countryDisplayManager = new CountryDisplayManager(factory, new CountryController(factory));
            sportDisplayManager = new SportDisplayManager(factory, new SportController(factory));

            countryDisplayManager.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            sportDisplayManager.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, String.Format("Welcome, {0} (admin)", login));
        }

        private void CreateCountry_Click(object sender, RoutedEventArgs e)
        {
            CreateCountry();
        }
        private void CreateCountry()
        {
            CountryCreateViewModel viewModel = new CountryCreateViewModel();
            CountryCreateControl control = new CountryCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.CountryCreated += (s, e) =>
            {
                using (ICountryService service = factory.CreateCountryService())
                {
                    ServiceMessage serviceMessage = service.Create(e.ContryName);

                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                        viewModel.CountryName = String.Empty;
                }
            };

            window.ShowDialog();
        }

        private void CreateSport_Click(object sender, RoutedEventArgs e)
        {
            CreateSport();
        }
        private void CreateSport()
        {
            SportCreateViewModel viewModel = new SportCreateViewModel();
            SportCreateControl control = new SportCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportCreated += (s, e) =>
            {
                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Create(e.SportName);

                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                        viewModel.SportName = String.Empty;
                }
            };

            window.ShowDialog();
        }

        private void ManageCountries_Click(object sender, RoutedEventArgs e)
        {
            countryDisplayManager.DisplayCountries();
        }

        private void ManageSports_Click(object sender, RoutedEventArgs e)
        {
            sportDisplayManager.DisplaySports();
        }

        private void SetFooterMessage(bool success, string message)
        {
            footer.StatusText = success ? "Success!" : "Fail or error!";
            footer.MessageText = message;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //MessageBox for question
            RaiseSignedOutEvent();
        }
    }
}
