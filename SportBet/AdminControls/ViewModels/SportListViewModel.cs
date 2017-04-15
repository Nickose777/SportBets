using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using System.Windows.Input;

namespace SportBet.AdminControls.ViewModels
{
    public class SportListViewModel : ObservableObject, IObserver
    {
        public event SportDisplayEventHandler SportSelected;
        public event SportDisplayEventHandler SportDeleteRequest;

        private readonly ISubject subject;
        private readonly FacadeBase<string> facade;

        private string selectedSport;

        public SportListViewModel(ISubject subject, FacadeBase<string> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Sports = new ObservableCollection<string>(facade.GetAll());
            this.EditSportCommand = new DelegateCommand(() => RaiseSportSelectedEvent(SelectedSport), obj => SelectedSport != null);
            this.DeleteSportCommand = new DelegateCommand(() => RaiseSportDeleteRequestEvent(SelectedSport), obj => SelectedSport != null);

            subject.Subscribe(this);
        }

        public ICommand EditSportCommand { get; private set; }

        public ICommand DeleteSportCommand { get; private set; }

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

        private void RaiseSportSelectedEvent(string sportName)
        {
            var handler = SportSelected;
            if (handler != null)
            {
                SportDisplayEventArgs e = new SportDisplayEventArgs(sportName);
                handler(this, e);
            }
        }

        private void RaiseSportDeleteRequestEvent(string sportName)
        {
            var handler = SportDeleteRequest;
            if (handler != null)
            {
                SportDisplayEventArgs e = new SportDisplayEventArgs(sportName);
                handler(this, e);
            }
        }
    }
}
