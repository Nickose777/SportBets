using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public class BetInfoViewModel : ObservableObject
    {
        public event BetEditEventHandler BetEdited;

        private BetEditModel bet;

        public BetInfoViewModel(BetDisplayModel betDisplayModel)
        {
            this.bet = new BetEditModel
            {
                ClientPhoneNumber = betDisplayModel.ClientPhoneNumber,
                CoefficientDescription = betDisplayModel.CoefficientDescription,
                DateOfEvent = betDisplayModel.DateOfEvent,
                EventParticipants = betDisplayModel.EventParticipants,
                SportName = betDisplayModel.SportName,
                TournamentName = betDisplayModel.TournamentName,

                NewSum = betDisplayModel.Sum,
                OldSum = betDisplayModel.Sum
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseBetEditedEvent(bet), obj => CanSaveChanges());
            this.UndoCommand = new DelegateCommand(Undo, obj => IsDirty());

            this.EventParticipants = new ObservableCollection<ParticipantBaseModel>(bet.EventParticipants);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string SportName
        {
            get { return bet.SportName; }
        }

        public string TournamentName
        {
            get { return bet.TournamentName; }
        }

        public DateTime DateOfEvent
        {
            get { return bet.DateOfEvent; }
        }

        public string CoefficientDescription
        {
            get { return bet.CoefficientDescription; }
        }

        public string ClientPhoneNumber
        {
            get { return bet.ClientPhoneNumber; }
        }

        public decimal Sum
        {
            get { return bet.NewSum; }
            set
            {
                bet.NewSum = value;
                RaisePropertyChangedEvent("Sum");
            }
        }

        public ObservableCollection<ParticipantBaseModel> EventParticipants { get; private set; }

        private bool CanSaveChanges()
        {
            return
                IsDirty() &&
                Sum > 0;
        }

        private bool IsDirty()
        {
            return bet.OldSum != Sum;
        }

        private void Undo()
        {
            Sum = bet.OldSum;
        }

        private void RaiseBetEditedEvent(BetEditModel betEditModel)
        {
            var handler = BetEdited;
            if (handler != null)
            {
                BetEditEventArgs e = new BetEditEventArgs(betEditModel);
                handler(this, e);
            }
        }
    }
}
