using System.Windows.Input;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ManageClientsViewModel : ObservableObject
    {
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

        public ManageClientsViewModel(IClientSubject subject, IClientController controller, bool allowDeleteClient)
        {
            ClientListViewModel = new ClientListViewModel(subject, controller);
            CanDeleteClient = allowDeleteClient;

            this.DeleteSelectedClientCommand = new DelegateCommand(
                () => RaiseClientDeleteRequestEvent(SelectedClient),
                obj => SelectedClient != null);
        }

        public ICommand DeleteSelectedClientCommand { get; private set; }

        public bool CanDeleteClient { get; private set; }

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
