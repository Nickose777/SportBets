using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Create;
using SportBet.Models.Create;
using SportBet.Models.Base;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentCreateViewModel : ObservableObject
    {
        public event TournamentEventHandler TournamentCreated;

        private TournamentBaseModel tournament;

        public TournamentCreateViewModel(IEnumerable<string> sportNames)
        {
            tournament = new TournamentBaseModel
            {
                DateOfStart = DateTime.Now.AddMonths(1)
            };

            this.CreateTournamentCommand = new DelegateCommand(() => RaiseTournamentCreatedEvent(tournament), CanCreateTournament);

            this.Sports = new ObservableCollection<string>(sportNames);
        }

        public ICommand CreateTournamentCommand { get; private set; }

        private bool CanCreateTournament(object parameter)
        {
            return
                !String.IsNullOrEmpty(Name) &&
                !String.IsNullOrEmpty(SportName) &&
                DateOfStart != null &&
                Name.Length <= 20;
        }

        public string Name
        {
            get { return tournament.Name; }
            set
            {
                tournament.Name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public string SportName
        {
            get { return tournament.SportName; }
            set
            {
                tournament.SportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        public DateTime DateOfStart
        {
            get { return tournament.DateOfStart; }
            set
            {
                tournament.DateOfStart = value;
                RaisePropertyChangedEvent("DateOfStart");
            }
        }

        public ObservableCollection<string> Sports { get; private set; }

        private void RaiseTournamentCreatedEvent(TournamentBaseModel tournament)
        {
            var handler = TournamentCreated;
            if (handler != null)
            {
                TournamentEventArgs e = new TournamentEventArgs(tournament);
                handler(this, e);
            }
        }
    }
}
