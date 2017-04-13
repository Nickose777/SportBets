using System;
using System.Windows.Input;
using SportBet.EventHandlers.Create;

namespace SportBet.AdminControls.ViewModels
{
    public class SportCreateViewModel : ObservableObject
    {
        public event SportEventHandler SportCreated;

        private string sportName;

        public SportCreateViewModel()
        {
            sportName = String.Empty;
            CreateSportCommand = new DelegateCommand(() => RaiseSportCreatedEvent(sportName), CanCreateSport);
        }

        public ICommand CreateSportCommand { get; private set; }

        public string SportName
        {
            get { return sportName; }
            set
            {
                sportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        private bool CanCreateSport(object parameter)
        {
            return 3 < SportName.Length && SportName.Length <= 20;
        }

        private void RaiseSportCreatedEvent(string sportName)
        {
            var handler = SportCreated;
            if (handler != null)
            {
                SportEventArgs e = new SportEventArgs(sportName);
                handler(this, e);
            }
        }
    }
}
