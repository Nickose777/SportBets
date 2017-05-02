using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public abstract class BetListViewModel : ObservableObject, IObserver
    {
        public event BetDisplayEventHandler BetSelected;

        private readonly FacadeBase<BetDisplayModel> facade;

        private BetDisplayModel selectedBet;

        public BetListViewModel(ISubject subject, FacadeBase<BetDisplayModel> facade)
        {
            this.facade = facade;

            this.EditBetCommand = new DelegateCommand(() => RaiseBetSelectedEvent(SelectedBet), obj => SelectedBet != null);

            this.Bets = new ObservableCollection<BetDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public ICommand EditBetCommand { get; private set; }

        public abstract bool HasEditPermissions { get; }

        public abstract bool HasDeletePermissions { get; }

        public BetDisplayModel SelectedBet
        {
            get { return selectedBet; }
            set
            {
                selectedBet = value;
                RaisePropertyChangedEvent("SelectedBet");
            }
        }

        public void Update()
        {
            IEnumerable<BetDisplayModel> bets = facade.GetAll();

            Bets.Clear();
            foreach (BetDisplayModel bet in bets)
            {
                Bets.Add(bet);
            }
        }

        public ObservableCollection<BetDisplayModel> Bets { get; private set; }

        private void RaiseBetSelectedEvent(BetDisplayModel betDisplayModel)
        {
            var handler = BetSelected;
            if (handler != null)
            {
                BetDisplayEventArgs e = new BetDisplayEventArgs(betDisplayModel);
                handler(this, e);
            }
        }
    }
}
