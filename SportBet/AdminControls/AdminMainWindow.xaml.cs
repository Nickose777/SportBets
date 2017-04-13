using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;
using SportBet.AdminControls.ViewModels;
using SportBet.AdminControls.UserControls;
using SportBet.WindowFactories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;

namespace SportBet.AdminControls
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MainWindowBase
    {
        public AdminMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

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
