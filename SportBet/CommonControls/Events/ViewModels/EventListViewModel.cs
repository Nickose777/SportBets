using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventListViewModel : ObservableObject, IObserver
    {
        public event EventDisplayEventHandler EventSelected;

        private readonly FacadeBase<EventDisplayModel> facade;

        private EventDisplayModel selectedEvent;

        public EventListViewModel(ISubject subject, FacadeBase<EventDisplayModel> facade)
        {
            this.facade = facade;

            this.SelectEventCommand = new DelegateCommand(() => RaiseEventSelectedEvent(SelectedEvent), obj => SelectedEvent != null);
            this.Events = new ObservableCollection<EventDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public ICommand SelectEventCommand { get; private set; }

        public EventDisplayModel SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                RaisePropertyChangedEvent("SelectedEvent");
            }
        }

        public void Update()
        {
            IEnumerable<EventDisplayModel> eventDisplayModels = facade.GetAll();

            Events.Clear();
            foreach (EventDisplayModel eventDisplayModel in eventDisplayModels)
            {
                Events.Add(eventDisplayModel);
            }
        }

        public ObservableCollection<EventDisplayModel> Events { get; private set; }

        private void RaiseEventSelectedEvent(EventDisplayModel _event)
        {
            var handler = EventSelected;
            if (handler != null)
            {
                EventDisplayEventArgs e = new EventDisplayEventArgs(_event);
                handler(this, e);
            }
        }
    }
}
