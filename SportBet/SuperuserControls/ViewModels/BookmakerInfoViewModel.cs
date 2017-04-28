using System;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.SuperuserControls.ViewModels
{
    public class BookmakerInfoViewModel : ObservableObject
    {
        public event BookmakerEditEventHandler BookmakerEdited;

        private readonly BookmakerEditModel bookmaker;
        private readonly BookmakerEditModel bookmakerForEdit;

        public BookmakerInfoViewModel(BookmakerEditModel bookmaker)
        {
            this.bookmaker = bookmaker;
            this.bookmakerForEdit = new BookmakerEditModel
            {
                FirstName = bookmaker.FirstName,
                LastName = bookmaker.LastName,
                PhoneNumber = bookmaker.PhoneNumber
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseBookmakerEditedEvent(bookmakerForEdit), CanSave);
            this.UndoCommand = new DelegateCommand(Undo, obj => IsDirty());
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string LastName
        {
            get { return bookmakerForEdit.LastName; }
            set
            {
                bookmakerForEdit.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }

        public string FirstName
        {
            get { return bookmakerForEdit.FirstName; }
            set
            {
                bookmakerForEdit.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }

        public string PhoneNumber
        {
            get { return bookmakerForEdit.PhoneNumber; }
            set
            {
                bookmakerForEdit.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void Undo()
        {
            FirstName = bookmaker.FirstName;
            LastName = bookmaker.LastName;
            PhoneNumber = bookmaker.PhoneNumber;
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
                bookmaker.FirstName != FirstName ||
                bookmaker.LastName != LastName ||
                bookmaker.PhoneNumber != PhoneNumber;
        }

        private void RaiseBookmakerEditedEvent(BookmakerEditModel bookmaker)
        {
            var handler = BookmakerEdited;
            if (handler != null)
            {
                BookmakerEditEventArgs e = new BookmakerEditEventArgs(bookmaker);
                handler(this, e);
            }
        }
    }
}
