using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;

namespace SportBet.AdminControls.ViewModels
{
    public class SportListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly ISportController controller;

        private string selectedSport;

        public SportListViewModel(ISubject subject, ISportController controller)
        {
            this.subject = subject;
            this.controller = controller;

            this.Sports = new ObservableCollection<string>(controller.GetAll());

            subject.Subscribe(this);
        }

        public void Update()
        {
            IEnumerable<string> sportNames = controller.GetAll();

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
