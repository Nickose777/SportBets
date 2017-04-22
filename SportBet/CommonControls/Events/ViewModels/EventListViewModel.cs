using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<EventDisplayModel> facade;

        private EventDisplayModel selectedEvent;

        public EventListViewModel(ISubject subject, FacadeBase<EventDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            subject.Subscribe(this);

            this.Events = new ObservableCollection<EventDisplayModel>(facade.GetAll());
        }

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
    }
}
