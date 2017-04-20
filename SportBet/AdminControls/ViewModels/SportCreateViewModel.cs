using System;
using System.Windows.Input;
using SportBet.EventHandlers.Create;
using SportBet.Models.Create;

namespace SportBet.AdminControls.ViewModels
{
    public class SportCreateViewModel : ObservableObject
    {
        public event SportEventHandler SportCreated;

        private SportCreateModel sport;

        public SportCreateViewModel()
        {
            sport = new SportCreateModel();
            CreateSportCommand = new DelegateCommand(() => RaiseSportCreatedEvent(sport), CanCreateSport);
        }

        public ICommand CreateSportCommand { get; private set; }

        public string SportName
        {
            get { return sport.SportName; }
            set
            {
                sport.SportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        public bool IsDual
        {
            get { return sport.IsDual; }
            set
            {
                sport.IsDual = value;
                RaisePropertyChangedEvent("IsDual");
            }
        }

        private bool CanCreateSport(object parameter)
        {
            return 
                !String.IsNullOrEmpty(SportName) &&
                3 < SportName.Length && SportName.Length <= 20;
        }

        private void RaiseSportCreatedEvent(SportCreateModel sport)
        {
            var handler = SportCreated;
            if (handler != null)
            {
                SportEventArgs e = new SportEventArgs(sport);
                handler(this, e);
            }
        }
    }
}
