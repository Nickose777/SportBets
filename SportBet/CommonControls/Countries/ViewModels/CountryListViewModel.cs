using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.EventHandlers.Display;

namespace SportBet.CommonControls.Countries.ViewModels
{
    public class CountryListViewModel : ObservableObject, IObserver
    {
        public event CountryDisplayEventHandler CountrySelected;
        public event CountryDisplayEventHandler CountryDeleteRequest;

        private readonly ISubject subject;
        private readonly FacadeBase<string> facade;

        private string selectedCountry;

        public CountryListViewModel(ISubject subject, FacadeBase<string> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Countries = new ObservableCollection<string>(facade.GetAll());
            this.EditCountryCommand = new DelegateCommand(() => RaiseCountrySelectedEvent(SelectedCountry), obj => SelectedCountry != null);
            this.DeleteCountryCommand = new DelegateCommand(() => RaiseCountryDeleteRequestEvent(SelectedCountry), obj => SelectedCountry != null);

            subject.Subscribe(this);
        }

        public ICommand EditCountryCommand { get; private set; }

        public ICommand DeleteCountryCommand { get; private set; }

        public void Update()
        {
            IEnumerable<string> countryNames = facade.GetAll();

            Countries.Clear();
            foreach (string countryName in countryNames)
            {
                Countries.Add(countryName);
            }

            RaisePropertyChangedEvent("SelectedCountry");
        }

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                RaisePropertyChangedEvent("SelectedCountry");
            }
        }

        public ObservableCollection<string> Countries { get; set; }

        private void RaiseCountrySelectedEvent(string countryName)
        {
            var handler = CountrySelected;
            if (handler != null)
            {
                CountryDisplayEventArgs e = new CountryDisplayEventArgs(countryName);
                handler(this, e);
            }
        }

        private void RaiseCountryDeleteRequestEvent(string countryName)
        {
            var handler = CountryDeleteRequest;
            if (handler != null)
            {
                CountryDisplayEventArgs e = new CountryDisplayEventArgs(countryName);
                handler(this, e);
            }
        }
    }
}
