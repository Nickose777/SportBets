using System;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.SuperuserControls.ViewModels
{
    public class AnalyticInfoViewModel : ObservableObject
    {
        public event AnalyticEditEventHandler AnalyticEdited;

        private readonly AnalyticEditModel analytic;
        private readonly AnalyticEditModel analyticForEdit;

        public AnalyticInfoViewModel(AnalyticEditModel analytic)
        {
            this.analytic = analytic;
            this.analyticForEdit = new AnalyticEditModel
            {
                FirstName = analytic.FirstName,
                LastName = analytic.LastName,
                PhoneNumber = analytic.PhoneNumber,
                Login = analytic.Login
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseAnalyticEditedEvent(analyticForEdit), CanSave);
            this.UndoCommand = new DelegateCommand(Undo, obj => IsDirty());
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string LastName
        {
            get { return analyticForEdit.LastName; }
            set
            {
                analyticForEdit.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }

        public string FirstName
        {
            get { return analyticForEdit.FirstName; }
            set
            {
                analyticForEdit.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }

        public string PhoneNumber
        {
            get { return analyticForEdit.PhoneNumber; }
            set
            {
                analyticForEdit.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void Undo()
        {
            FirstName = analytic.FirstName;
            LastName = analytic.LastName;
            PhoneNumber = analytic.PhoneNumber;
        }

        private bool CanSave(object parameter)
        {
            return
                IsDirty() &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber);
        }

        private bool IsDirty()
        {
            return
                analytic.FirstName != FirstName ||
                analytic.LastName != LastName ||
                analytic.PhoneNumber != PhoneNumber;
        }

        private void RaiseAnalyticEditedEvent(AnalyticEditModel analytic)
        {
            var handler = AnalyticEdited;
            if (handler != null)
            {
                AnalyticEditEventArgs e = new AnalyticEditEventArgs(analytic);
                handler(this, e);
            }
        }
    }
}
