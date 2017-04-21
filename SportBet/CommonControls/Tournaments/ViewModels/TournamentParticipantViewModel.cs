using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Base;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentParticipantViewModel : ObservableObject
    {
        public event TournamentEditEventHandler TournamentEdited;

        private readonly TournamentEditModel tournament;
        private ParticipantBaseModel selectedParticipant;
        private ParticipantBaseModel selectedTournamentParticipant;

        public TournamentParticipantViewModel(TournamentEditModel tournament, IEnumerable<ParticipantBaseModel> sportParticipants)
        {
            this.tournament = tournament;

            this.SaveCommand = new DelegateCommand(() => Save(tournament), obj => true);

            this.MoveToTournamentCommand = new DelegateCommand(MoveToTournament, obj => SelectedParticipant != null);
            this.MoveAllToTournamentCommand = new DelegateCommand(MoveAllToTournament, obj => true);
            this.MoveFromTournamentCommand = new DelegateCommand(MoveFromTournament, obj => SelectedTournamentParticipant != null);

            this.SportParticipants = new ObservableCollection<ParticipantBaseModel>(sportParticipants);
            this.TournamentParticipants = new ObservableCollection<ParticipantBaseModel>(tournament.Participants);

            SortedAllParticipants.Filter = obj =>
            {
                ParticipantBaseModel participant = obj as ParticipantBaseModel;
                return TournamentParticipants
                    .Count(part => 
                        part.CountryName == participant.CountryName &&
                        part.Name == participant.Name
                        ) == 0;
            };
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand MoveToTournamentCommand { get; private set; }

        public ICommand MoveAllToTournamentCommand { get; private set; }

        public ICommand MoveFromTournamentCommand { get; private set; }

        public ParticipantBaseModel SelectedParticipant
        {
            get { return selectedParticipant; }
            set
            {
                selectedParticipant = value;
                RaisePropertyChangedEvent("SelectedParticipant");
            }
        }

        public ParticipantBaseModel SelectedTournamentParticipant
        {
            get { return selectedTournamentParticipant; }
            set
            {
                selectedTournamentParticipant = value;
                RaisePropertyChangedEvent("SelectedTournamentParticipant");
            }
        }

        public ICollectionView SortedAllParticipants
        {
            get { return CollectionViewSource.GetDefaultView(SportParticipants); }
        }

        public ObservableCollection<ParticipantBaseModel> SportParticipants { get; private set; }

        public ObservableCollection<ParticipantBaseModel> TournamentParticipants { get; private set; }

        private void MoveToTournament()
        {
            TournamentParticipants.Add(SelectedParticipant);
            SortedAllParticipants.Refresh();
        }

        private void MoveAllToTournament()
        {
            foreach (var obj in SortedAllParticipants)
            {
                ParticipantBaseModel participant = obj as ParticipantBaseModel;
                TournamentParticipants.Add(participant);
            }

            SortedAllParticipants.Refresh();
        }

        private void MoveFromTournament()
        {
            TournamentParticipants.Remove(SelectedTournamentParticipant);
            SortedAllParticipants.Refresh();
        }

        private void Save(TournamentEditModel tournament)
        {
            tournament.Participants.Clear();

            foreach (ParticipantBaseModel participant in TournamentParticipants)
            {
                tournament.Participants.Add(participant);
            }

            RaiseTournamentEditedEvent(tournament);
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
