using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;

namespace SportBet.AdminControls.ViewModels
{
    public class SportListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<string> facade;

        private string selectedSport;

        public SportListViewModel(ISubject subject, FacadeBase<string> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Sports = new ObservableCollection<string>(facade.GetAll());

            subject.Subscribe(this);
        }

        public void Update()
        {
            IEnumerable<string> sportNames = facade.GetAll();

            Sports.Clear();
            foreach (string sportName in sportNames)
            {
                Sports.Add(sportName);
            }

            RaisePropertyChangedEvent("SelectedSport");
        }

        public string SelectedSport
        {
            get { return selectedSport; }
            set
            {
                selectedSport = value;
                RaisePropertyChangedEvent("SelectedSport");
            }
        }

        public ObservableCollection<string> Sports { get; set; }
    }
}
