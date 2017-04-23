using System;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventInfoViewModel : ObservableObject
    {
        public event EventEditEventHandler EventEdited;

        private readonly EventEditModel eventEditModel;

        public EventInfoViewModel(EventDisplayModel eventDisplayModel)
        {
            this.eventEditModel = new EventEditModel
            {
                DateOfTournamentStart = eventDisplayModel.DateOfTournamentStart,
                SportName = eventDisplayModel.SportName,
                TournamentName = eventDisplayModel.TournamentName,

                DateOfEvent = eventDisplayModel.DateOfEvent,
                NewDateOfEvent = eventDisplayModel.DateOfEvent,

                OldNotes = eventDisplayModel.Notes,
                NewNotes = eventDisplayModel.Notes,

                Participants = eventDisplayModel.Participants
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseEventEditedEvent(eventEditModel), CanSave);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public string SportName
        {
            get { return eventEditModel.SportName; }
        }

        public string TournamentName
        {
            get { return eventEditModel.TournamentName; }
        }

        public DateTime TournamentDateOfStart
        {
            get { return eventEditModel.DateOfTournamentStart; }
        }

        public DateTime DateOfEvent
        {
            get { return eventEditModel.NewDateOfEvent; }
            set
            {
                eventEditModel.NewDateOfEvent = value;
                RaisePropertyChangedEvent("DateOfEvent");
            }
        }

        public string Notes
        {
            get { return eventEditModel.NewNotes; }
            set
            {
                eventEditModel.NewNotes = value;
                RaisePropertyChangedEvent("Notes");
            }
        }

        private bool CanSave(object parameter)
        {
            return
                IsDirty() &&
                !String.IsNullOrEmpty(Notes) &&
                Notes.Length <= 100 &&
                DateOfEvent >= TournamentDateOfStart;
        }

        private bool IsDirty()
        {
            return
                eventEditModel.DateOfEvent != eventEditModel.NewDateOfEvent ||
                eventEditModel.OldNotes != eventEditModel.NewNotes;
        }

        private void RaiseEventEditedEvent(EventEditModel _event)
        {
            var handler = EventEdited;
            if (handler != null)
            {
                EventEditEventArgs e = new EventEditEventArgs(_event);
                handler(this, e);
            }
        }
    }
}
