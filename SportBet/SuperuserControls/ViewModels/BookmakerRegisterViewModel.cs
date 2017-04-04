using SportBet.EventHandlers.Register;
using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.SuperuserControls.ViewModels
{
    public class BookmakerRegisterViewModel : RegisterObservableObject
    {
        public event BookmakerRegisterEventHandler BookmakerCreated;

        private readonly BookmakerRegisterModel bookmaker;

        public BookmakerRegisterViewModel(BookmakerRegisterModel model)
            : base(model)
        {
            bookmaker = model;
            CreateBookmakerCommand = new DelegateCommand(() => RaiseBookmakerCreatedEvent(bookmaker), CanCreateBookmaker);
        }

        public ICommand CreateBookmakerCommand { get; private set; }
        private bool CanCreateBookmaker(object obj)
        {
            return
                CanCreateModel() &&
                !String.IsNullOrEmpty(bookmaker.FirstName) &&
                !String.IsNullOrEmpty(bookmaker.LastName) &&
                !String.IsNullOrEmpty(bookmaker.PhoneNumber);
        }

        public string LastName
        {
            get { return bookmaker.LastName; }
            set
            {
                bookmaker.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }
        public string FirstName
        {
            get { return bookmaker.FirstName; }
            set
            {
                bookmaker.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }
        public string PhoneNumber
        {
            get { return bookmaker.PhoneNumber; }
            set
            {
                bookmaker.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void RaiseBookmakerCreatedEvent(BookmakerRegisterModel bookmaker)
        {
            var handler = BookmakerCreated;
            if (handler != null)
            {
                BookmakerEventArgs e = new BookmakerEventArgs(bookmaker);
                handler(this, e);
            }
        }
    }
}
