using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using SportBet.EventHandlers.Display;
using System.Windows.Input;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentListViewModel : ObservableObject, IObserver
    {
        public event TournamentDisplayEventHandler TournamentSelected;

        private readonly ISubject subject;
        private readonly FacadeBase<TournamentDisplayModel> facade;

        private TournamentDisplayModel selectedTournament;

        public TournamentListViewModel(ISubject subject, FacadeBase<TournamentDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.SelectTournamentCommand = new DelegateCommand(
                () => RaiseTournamentSelectedEvent(SelectedTournament),
                obj => SelectedTournament != null);

            this.Tournaments = new ObservableCollection<TournamentDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public ICommand SelectTournamentCommand { get; private set; }

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

        public void RaiseTournamentSelectedEvent(TournamentDisplayModel tournament)
        {
            var handler = TournamentSelected;
            if (handler != null)
            {
                TournamentDisplayEventArgs e = new TournamentDisplayEventArgs(tournament);
                handler(this, e);
            }
        }
    }
}
