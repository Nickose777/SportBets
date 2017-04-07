using SportBet.EventHandlers.Display;
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
                () => RaiseBookmakerDeletedEvent(SelectedBookmaker),
                obj => SelectedBookmaker != null);

            RaisePropertyChangedEvent("Bookmakers");
            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public void Refresh(IEnumerable<BookmakerDisplayModel> bookmakers)
        {
            Bookmakers.Clear();
            foreach (BookmakerDisplayModel bookmaker in bookmakers)
            {
                Bookmakers.Add(bookmaker);
            }

            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public ICommand DeleteSelectedBookmakerCommand { get; private set; }

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
