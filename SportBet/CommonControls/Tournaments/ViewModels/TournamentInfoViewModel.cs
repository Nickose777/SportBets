using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentInfoViewModel : ObservableObject
    {
        public event TournamentEditEventHandler TournamentEdited;

        private readonly TournamentEditModel tournament;

        public TournamentInfoViewModel(TournamentDisplayModel tournament, IEnumerable<string> sports)
        {
            this.tournament = new TournamentEditModel
            {
                OldName = tournament.Name,
                NewName = tournament.Name,

                OldSportName = tournament.SportName,
                NewSportName = tournament.SportName,

                OldDateOfStart = tournament.DateOfStart,
                NewDateOfStart = tournament.DateOfStart
            };

            this.Sports = new ObservableCollection<string>(sports);

            this.SaveTournamentCommand = new DelegateCommand(() => RaiseTournamentEditedEvent(this.tournament), CanSave);
            this.UndoCommand = new DelegateCommand(() => Undo(), IsDirty);
        }

        public ICommand SaveTournamentCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string Name
        {
            get { return tournament.NewName; }
            set
            {
                tournament.NewName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public string SportName
        {
            get { return tournament.NewSportName; }
            set
            {
                tournament.NewSportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        public DateTime DateOfStart
        {
            get { return tournament.NewDateOfStart; }
            set
            {
                tournament.NewDateOfStart = value;
                RaisePropertyChangedEvent("DateOfStart");
            }
        }

        public ObservableCollection<string> Sports { get; private set; }

        private bool CanSave(object parameter)
        {
            return
                IsDirty(parameter) &&
                !String.IsNullOrEmpty(Name) &&
                !String.IsNullOrEmpty(SportName) &&
                DateOfStart != null &&
                Name.Length <= 20;
        }

        private void Undo()
        {
            Name = tournament.OldName;
            SportName = tournament.OldSportName;
            DateOfStart = tournament.OldDateOfStart;
        }

        private bool IsDirty(object parameter)
        {
            return
                tournament.OldName != tournament.NewName ||
                tournament.OldSportName != tournament.NewSportName ||
                tournament.OldDateOfStart != tournament.NewDateOfStart;
        }

        private void RaiseTournamentEditedEvent(TournamentEditModel tournament)
        {
            var handler = TournamentEdited;
            if (handler != null)
            {
                TournamentEditEventArgs e = new TournamentEditEventArgs(tournament);
                handler(this, e);
            }
        }
    }
}
