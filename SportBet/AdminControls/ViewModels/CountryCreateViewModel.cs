using System;
using System.Windows.Input;
using SportBet.EventHandlers.Create;

namespace SportBet.AdminControls.ViewModels
{
    public class CountryCreateViewModel : ObservableObject
    {
        public event CountryEventHandler CountryCreated;

        private string countryName;

        public CountryCreateViewModel()
        {
            countryName = String.Empty;
            CreateCountryCommand = new DelegateCommand(() => RaiseCountryCreatedEvent(countryName), CanCreateCountry);
        }

        public ICommand CreateCountryCommand { get; private set; }

        public string CountryName
        {
            get { return countryName; }
            set
            {
                countryName = value;
                RaisePropertyChangedEvent("CountryName");
            }
        }

        private bool CanCreateCountry(object parameter)
        {
            return 3 < CountryName.Length && CountryName.Length <= 20;
        }
        
        private void RaiseCountryCreatedEvent(string countryName)
        {
            var handler = CountryCreated;
            if (handler != null)
            {
                CountryEventArgs e = new CountryEventArgs(countryName);
                handler(this, e);
            }
        }
    }
}
