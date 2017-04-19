using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<TournamentDisplayModel> facade;

        private TournamentDisplayModel selectedTournament;

        public TournamentListViewModel(ISubject subject, FacadeBase<TournamentDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;
            this.Tournaments = new ObservableCollection<TournamentDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public void Update()
        {
            IEnumerable<TournamentDisplayModel> tournaments = facade.GetAll();

            Tournaments.Clear();
            foreach (TournamentDisplayModel tournament in tournaments)
            {
                Tournaments.Add(tournament);
            }

            RaisePropertyChangedEvent("SelectedTournament");
        }

        public TournamentDisplayModel SelectedTournament
        {
            get { return selectedTournament; }
            set
            {
                selectedTournament = value;
                RaisePropertyChangedEvent("SelectedTournament");
            }
        }

        public ObservableCollection<TournamentDisplayModel> Tournaments { get; private set; }
    }
}
