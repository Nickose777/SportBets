using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Participants.ViewModels
{
    public class ParticipantListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<ParticipantDisplayModel> facade;

        private ParticipantDisplayModel selectedParticipant;

        public ParticipantListViewModel(ISubject subject, FacadeBase<ParticipantDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;
            this.Participants = new ObservableCollection<ParticipantDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public void Update()
        {
            IEnumerable<ParticipantDisplayModel> participants = facade.GetAll();

            Participants.Clear();
            foreach (ParticipantDisplayModel participant in participants)
            {
                Participants.Add(participant);
            }

            RaisePropertyChangedEvent("SelectedParticipant");
        }

        public ParticipantDisplayModel SelectedParticipant
        {
            get { return selectedParticipant; }
            set
            {
                selectedParticipant = value;
                RaisePropertyChangedEvent("SelectedParticipant");
            }
        }

        public ObservableCollection<ParticipantDisplayModel> Participants { get; private set; }
    }
}
