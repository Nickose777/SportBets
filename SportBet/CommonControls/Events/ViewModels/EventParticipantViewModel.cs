using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventParticipantViewModel : ObservableObject
    {
        public event EventEditEventHandler EventEdited;

        private readonly EventEditModel eventEditModel;

        private ParticipantBaseModel selectedParticipant;
        private ParticipantBaseModel selectedEventsParticipant;

        public EventParticipantViewModel(EventDisplayModel eventDisplayModel, IEnumerable<ParticipantBaseModel> allParticipants)
        {
            this.eventEditModel = new EventEditModel
            {
                DateOfTournamentStart = eventDisplayModel.DateOfTournamentStart,
                SportName = eventDisplayModel.SportName,
                TournamentName = eventDisplayModel.TournamentName,
                DateOfEvent = eventDisplayModel.DateOfEvent,
                OldNotes = eventDisplayModel.Notes,

                Participants = eventDisplayModel.Participants.ToList()
            };

            this.MoveToEventCommand = new DelegateCommand(
                () => MoveToEvent(SelectedParticipant),
                obj => SelectedParticipant != null);
            this.MoveAllToEventCommand = new DelegateCommand(
                () => MoveAllToEvent(),
                obj => true);
            this.MoveFromEventCommand = new DelegateCommand(
                () => MoveFromEvent(SelectedEventsParticipant),
                obj => SelectedEventsParticipant != null);
            this.SaveChangesCommand = new DelegateCommand(() => SaveChanges(eventEditModel), obj => true);

            this.AllParticipants = new ObservableCollection<ParticipantBaseModel>(allParticipants);
            this.SelectedParticipants = new ObservableCollection<ParticipantBaseModel>(eventDisplayModel.Participants);

            NotSelectedParticipants.Filter = obj =>
            {
                ParticipantBaseModel participant = obj as ParticipantBaseModel;
                return SelectedParticipants.Count(p =>
                        p.Name == participant.Name &&
                        p.CountryName == participant.CountryName &&
                        p.SportName == participant.SportName) == 0;
            };
        }

        public ICommand MoveToEventCommand { get; private set; }

        public ICommand MoveAllToEventCommand { get; private set; }

        public ICommand MoveFromEventCommand { get; private set; }

        public ICommand SaveChangesCommand { get; private set; }

        public ParticipantBaseModel SelectedParticipant
        {
            get { return selectedParticipant; }
            set
            {
                selectedParticipant = value;
                RaisePropertyChangedEvent("SelectedParticipant");
            }
        }

        public ParticipantBaseModel SelectedEventsParticipant
        {
            get { return selectedEventsParticipant; }
            set
            {
                selectedEventsParticipant = value;
                RaisePropertyChangedEvent("SelectedEventsParticipant");
            }
        }

        public ICollectionView NotSelectedParticipants
        {
            get { return CollectionViewSource.GetDefaultView(AllParticipants); }
        }

        public ObservableCollection<ParticipantBaseModel> AllParticipants { get; private set; }

        public ObservableCollection<ParticipantBaseModel> SelectedParticipants { get; private set; }

        private void MoveToEvent(ParticipantBaseModel participant)
        {
            SelectedParticipants.Add(participant);
            NotSelectedParticipants.Refresh();
        }

        private void MoveAllToEvent()
        {
            SelectedParticipants.Clear();

            foreach (ParticipantBaseModel participant in AllParticipants)
            {
                SelectedParticipants.Add(participant);
            }

            NotSelectedParticipants.Refresh();
        }

        private void MoveFromEvent(ParticipantBaseModel participant)
        {
            SelectedParticipants.Remove(participant);
            NotSelectedParticipants.Refresh();
        }

        private void SaveChanges(EventEditModel eventEditModel)
        {
            eventEditModel.NewParticipants = new List<ParticipantBaseModel>(SelectedParticipants);
            RaiseEventEditedEvent(eventEditModel);
        }

        private void RaiseEventEditedEvent(EventEditModel eventEditModel)
        {
            var handler = EventEdited;
            if (handler != null)
            {
                EventEditEventArgs e = new EventEditEventArgs(eventEditModel);
                handler(this, e);
            }
        }
    }
}
