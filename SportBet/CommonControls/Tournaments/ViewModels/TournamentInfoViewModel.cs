using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Base;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentInfoViewModel : ObservableObject
    {
        public event TournamentEditEventHandler TournamentEdited;

        private readonly TournamentEditModel tournament;

        public TournamentInfoViewModel(TournamentBaseModel tournament)
        {
            this.tournament = new TournamentEditModel
            {
                SportName = tournament.SportName,

                Name = tournament.Name,
                NewName = tournament.Name,

                DateOfStart = tournament.DateOfStart,
                NewDateOfStart = tournament.DateOfStart
            };

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

        public DateTime DateOfStart
        {
            get { return tournament.NewDateOfStart; }
            set
            {
                tournament.NewDateOfStart = value;
                RaisePropertyChangedEvent("DateOfStart");
            }
        }

        public ObservableCollection<string> Participants { get; private set; }

        private bool CanSave(object parameter)
        {
            return
                IsDirty(parameter) &&
                !String.IsNullOrEmpty(Name) &&
                DateOfStart != null &&
                Name.Length <= 20;
        }

        private void Undo()
        {
            Name = tournament.Name;
            DateOfStart = tournament.DateOfStart;
        }

        private bool IsDirty(object parameter)
        {
            return
                tournament.Name != tournament.NewName ||
                tournament.DateOfStart != tournament.NewDateOfStart;
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
