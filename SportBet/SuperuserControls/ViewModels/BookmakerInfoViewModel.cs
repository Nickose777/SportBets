using System;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.SuperuserControls.ViewModels
{
    public class BookmakerInfoViewModel : ObservableObject
    {
        public event BookmakerEditEventHandler BookmakerEdited;

        private bool isEditMode;

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

            IsEditMode = false;

            StartEditBookmakerCommand = new DelegateCommand(() => StartEdit(), obj => !isEditMode);
            CancelEditBookmakerCommand = new DelegateCommand(() => CancelEdit(), obj => isEditMode);
            SaveBookmakerCommand = new DelegateCommand(() => Save(), CanSave);
        }

        public ICommand StartEditBookmakerCommand { get; private set; }

        public ICommand CancelEditBookmakerCommand { get; private set; }

        public ICommand SaveBookmakerCommand { get; private set; }

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

        public bool IsEditMode
        {
            get { return isEditMode; }
            private set 
            { 
                isEditMode = value;
                RaisePropertyChangedEvent("IsEditMode");
            }
        }

        private void StartEdit()
        {
            IsEditMode = true;
        }

        private void CancelEdit()
        {
            IsEditMode = false;
            FirstName = bookmaker.FirstName;
            LastName = bookmaker.LastName;
            PhoneNumber = bookmaker.PhoneNumber;
        }

        private void Save()
        {
            RaiseBookmakerEditedEvent(bookmakerForEdit);
            IsEditMode = false;
        }

        private bool CanSave(object parameter)
        {
            return
                isEditMode &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber);
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
