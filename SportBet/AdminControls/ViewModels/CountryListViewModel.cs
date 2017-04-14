using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;

namespace SportBet.AdminControls.ViewModels
{
    public class CountryListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<string> facade;

        private string selectedCountry;

        public CountryListViewModel(ISubject subject, FacadeBase<string> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Countries = new ObservableCollection<string>(facade.GetAll());

            subject.Subscribe(this);
        }

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
    }
}
