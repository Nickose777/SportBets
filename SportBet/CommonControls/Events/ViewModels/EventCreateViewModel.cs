using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.EventHandlers.Create;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Extra;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventCreateViewModel : ObservableObject
    {
        public event EventCreateEventHandler EventCreated;

        private readonly EventCreateModel eventCreateModel;

        private string selectedSport;
        private TournamentBaseModel selectedTournament;
        private ParticipantTournamentModel selectedParticipant;
        private ParticipantTournamentModel selectedEventsParticipant;

        public EventCreateViewModel(IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<ParticipantTournamentModel> participants)
        {
            this.eventCreateModel = new EventCreateModel
            {
                DateOfEvent = DateTime.Now.AddMonths(2).Date
            };

            this.Sports = new ObservableCollection<string>(sports);
            this.Tournaments = new ObservableCollection<TournamentBaseModel>(tournaments);
            this.Participants = new ObservableCollection<ParticipantTournamentModel>(participants);
            this.SelectedParticipants = new ObservableCollection<ParticipantTournamentModel>();

            this.CreateEventCommand = new DelegateCommand(() => Create(eventCreateModel), CanCreate);
            this.MoveToEventCommand = new DelegateCommand(
                () => MoveToEvent(SelectedParticipant), 
                obj => SelectedParticipant != null);
            this.MoveAllToEventCommand = new DelegateCommand(
                () => MoveAllToEvent(), 
                obj => true);
            this.MoveFromEventCommand = new DelegateCommand(
                () => MoveFromEvent(SelectedEventsParticipant), 
                obj => SelectedEventsParticipant != null);

            this.SortedTournaments.Filter = obj =>
            {
                TournamentBaseModel tournamentBaseModel = obj as TournamentBaseModel;
                return tournamentBaseModel.SportName == SelectedSport;
            };
            this.SortedParticipants.Filter = obj =>
            {
                bool isValid = SelectedTournament != null;

                if (isValid)
                {
                    ParticipantTournamentModel participantModel = obj as ParticipantTournamentModel;
                    isValid = participantModel
                            .Tournaments
                            .Select(t => t.Name)
                            .Contains(SelectedTournament.Name) &&
                        !SelectedParticipants
                            .Select(p => p.Name)
                            .Contains(participantModel.Name);
                }

                return isValid;
            };
        }

        public ICommand CreateEventCommand { get; private set; }

        public ICommand MoveToEventCommand { get; private set; }

        public ICommand MoveAllToEventCommand { get; private set; }

        public ICommand MoveFromEventCommand { get; private set; }

        public string SelectedSport
        {
            get { return selectedSport; }
            set
            {
                selectedSport = value;
                RaisePropertyChangedEvent("SelectedSport");
                SortedTournaments.Refresh();
            }
        }

        public TournamentBaseModel SelectedTournament
        {
            get { return selectedTournament; }
            set
            {
                selectedTournament = value;

                SelectedParticipants.Clear();
                SortedParticipants.Refresh();

                RaisePropertyChangedEvent("SelectedTournament");
            }
        }

        public ParticipantTournamentModel SelectedParticipant
        {
            get { return selectedParticipant; }
            set
            {
                selectedParticipant = value;
                RaisePropertyChangedEvent("SelectedParticipant");
            }
        }

        public ParticipantTournamentModel SelectedEventsParticipant
        {
            get { return selectedEventsParticipant; }
            set
            {
                selectedEventsParticipant = value;
                RaisePropertyChangedEvent("SelectedEventsParticipant");
            }
        }

        public string Notes
        {
            get { return eventCreateModel.Notes; }
            set
            {
                eventCreateModel.Notes = value;
                RaisePropertyChangedEvent("Notes");
            }
        }

        public DateTime DateOfEvent
        {
            get { return eventCreateModel.DateOfEvent; }
            set
            {
                eventCreateModel.DateOfEvent = value;
                RaisePropertyChangedEvent("DateOfEvent");
            }
        }

        public ICollectionView SortedTournaments
        {
            get { return CollectionViewSource.GetDefaultView(Tournaments); }
        }

        public ICollectionView SortedParticipants
        {
            get { return CollectionViewSource.GetDefaultView(Participants); }
        }

        public ObservableCollection<string> Sports { get; private set; }

        public ObservableCollection<TournamentBaseModel> Tournaments { get; private set; }

        public ObservableCollection<ParticipantTournamentModel> Participants { get; private set; }

        public ObservableCollection<ParticipantTournamentModel> SelectedParticipants { get; private set; }

        private void MoveToEvent(ParticipantTournamentModel participant)
        {
            SelectedParticipants.Add(participant);
            SortedParticipants.Refresh();
        }

        private void MoveAllToEvent()
        {
            SelectedParticipants.Clear();

            foreach (ParticipantTournamentModel participant in Participants)
            {
                SelectedParticipants.Add(participant);
            }

            SortedParticipants.Refresh();
        }

        private void MoveFromEvent(ParticipantTournamentModel participant)
        {
            SelectedParticipants.Remove(participant);
            SortedParticipants.Refresh();
        }

        private bool CanCreate(object obj)
        {
            return
                SelectedSport != null &&
                SelectedTournament != null &&
                !String.IsNullOrEmpty(Notes) &&
                DateOfEvent != default(DateTime) &&
                SelectedParticipants.Count > 1;

            //TODO sport.isDual
        }

        private void Create(EventCreateModel eventCreateModel)
        {
            eventCreateModel.SportName = SelectedSport;
            eventCreateModel.TournamentName = SelectedTournament.Name;
            eventCreateModel.DateOfTournamentStart = SelectedTournament.DateOfStart;
            eventCreateModel.Participants = new List<ParticipantBaseModel>(SelectedParticipants);

            RaiseEventCreatedEvent(eventCreateModel);
        }

        private void RaiseEventCreatedEvent(EventCreateModel eventCreateModel)
        {
            var handler = EventCreated;
            if (handler != null)
            {
                EventEventArgs e = new EventEventArgs(eventCreateModel);
                handler(this, e);
            }
        }
    }
}
