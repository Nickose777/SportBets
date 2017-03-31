using SportBet.EventHandlers;
using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageBookmakersViewModel : ObservableObject
    {
        public event BookmakerDisplayEventHandler BookmakerDeleted;

        private BookmakerDisplayModel bookmaker;

        public ManageBookmakersViewModel(IEnumerable<BookmakerDisplayModel> bookmakers)
        {
            this.Bookmakers = new ObservableCollection<BookmakerDisplayModel>(bookmakers);

            this.DeleteSelectedBookmakerCommand = new DelegateCommand(
                () => DeleteBookmaker(SelectedBookmaker),
                obj => SelectedBookmaker != null);

            RaisePropertyChangedEvent("Bookmakers");
            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public ICommand DeleteSelectedBookmakerCommand { get; private set; }

        private void DeleteBookmaker(BookmakerDisplayModel bookmaker)
        {
            RaiseBookmakerDeletedEvent(bookmaker);
            Bookmakers.Remove(bookmaker);

            RaisePropertyChangedEvent("Bookmakers");
            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public BookmakerDisplayModel SelectedBookmaker
        {
            get { return bookmaker; }
            set
            {
                bookmaker = value;
                RaisePropertyChangedEvent("SelectedBookmaker");
            }
        }

        public ObservableCollection<BookmakerDisplayModel> Bookmakers { get; private set; }

        private void RaiseBookmakerDeletedEvent(BookmakerDisplayModel bookmaker)
        {
            var handler = BookmakerDeleted;
            if (handler != null)
            {
                BookmakerDisplayEventArgs e = new BookmakerDisplayEventArgs(bookmaker);
                handler(this, e);
            }
        }
    }
}
