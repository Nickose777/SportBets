using System.Windows.Input;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;
using SportBet.Contracts;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageBookmakersViewModel : ObservableObject
    {
        public event BookmakerDisplayEventHandler BookmakerSelectRequest;
        public event BookmakerDisplayEventHandler BookmakerDeleteRequest;

        public BookmakerListViewModel BookmakerListViewModel { get; private set; }

        public BookmakerDisplayModel SelectedBookmaker
        {
            get { return BookmakerListViewModel.SelectedBookmaker; }
            set
            {
                BookmakerListViewModel.SelectedBookmaker = value;
                RaisePropertyChangedEvent("SelectedBookmaker");
            }
        }

        public ManageBookmakersViewModel(ISubject subject, FacadeBase<BookmakerDisplayModel> facade)
        {
            BookmakerListViewModel = new BookmakerListViewModel(subject, facade);

            this.SelectBookmakerCommand = new DelegateCommand(
                () => RaiseBookmakerSelectRequestEvent(SelectedBookmaker),
                obj => SelectedBookmaker != null);
            this.DeleteBookmakerCommand = new DelegateCommand(
                () => RaiseBookmakerDeleteRequestEvent(SelectedBookmaker),
                obj => SelectedBookmaker != null);
        }

        public ICommand SelectBookmakerCommand { get; private set; }

        public ICommand DeleteBookmakerCommand { get; private set; }

        private void RaiseBookmakerSelectRequestEvent(BookmakerDisplayModel bookmaker)
        {
            var handler = BookmakerSelectRequest;
            if (handler != null)
            {
                BookmakerDisplayEventArgs e = new BookmakerDisplayEventArgs(bookmaker);
                handler(this, e);
            }
        }

        private void RaiseBookmakerDeleteRequestEvent(BookmakerDisplayModel bookmaker)
        {
            var handler = BookmakerDeleteRequest;
            if (handler != null)
            {
                BookmakerDisplayEventArgs e = new BookmakerDisplayEventArgs(bookmaker);
                handler(this, e);
            }
        }
    }
}
