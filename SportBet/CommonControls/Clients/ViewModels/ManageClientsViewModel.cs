using System.Windows.Input;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;
using SportBet.Contracts;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public abstract class ManageClientsViewModel : ObservableObject
    {
        public event ClientDisplayEventHandler ClientSelectRequest;
        public event ClientDisplayEventHandler ClientDeleteRequest;

        public ClientListViewModel ClientListViewModel { get; private set; }

        public ClientDisplayModel SelectedClient
        {
            get { return ClientListViewModel.SelectedClient; }
            set
            {
                ClientListViewModel.SelectedClient = value;
                RaisePropertyChangedEvent("SelectedClient");
            }
        }

        public ManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
        {
            ClientListViewModel = new ClientListViewModel(subject, facade);

            this.SelectClientCommand = new DelegateCommand(
                () => RaiseClientSelectRequestEvent(SelectedClient),
                obj => SelectedClient != null);
            this.DeleteClientCommand = new DelegateCommand(
                () => RaiseClientDeleteRequestEvent(SelectedClient),
                obj => SelectedClient != null);
        }

        public ICommand SelectClientCommand { get; private set; }

        public ICommand DeleteClientCommand { get; private set; }

        public abstract bool HasDeletePermissions { get; }

        private void RaiseClientSelectRequestEvent(ClientDisplayModel client)
        {
            var handler = ClientSelectRequest;
            if (handler != null)
            {
                ClientDisplayEventArgs e = new ClientDisplayEventArgs(client);
                handler(this, e);
            }
        }

        private void RaiseClientDeleteRequestEvent(ClientDisplayModel client)
        {
            var handler = ClientDeleteRequest;
            if (handler != null)
            {
                ClientDisplayEventArgs e = new ClientDisplayEventArgs(client);
                handler(this, e);
            }
        }
    }
}
