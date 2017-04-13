using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;

namespace SportBet.AdminControls.ViewModels
{
    public class CountryListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly ICountryController controller;

        private string selectedCountry;

        public CountryListViewModel(ISubject subject, ICountryController controller)
        {
            this.subject = subject;
            this.controller = controller;

            this.Countries = new ObservableCollection<string>(controller.GetAll());

            subject.Subscribe(this);
        }

        public void Update()
        {
            IEnumerable<string> countryNames = controller.GetAll();

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
    }
}
