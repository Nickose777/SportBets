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
    public class ManageClientsViewModel : ObservableObject
    {
        public event ClientDisplayEventHandler ClientDeleted;

        private ClientDisplayModel client;

        public ManageClientsViewModel(IEnumerable<ClientDisplayModel> clients)
        {
            this.Clients = new ObservableCollection<ClientDisplayModel>(clients);

            this.DeleteSelectedClientCommand = new DelegateCommand(
                () => RaiseClientDeletedEvent(SelectedClient),
                obj => SelectedClient != null);

            RaisePropertyChangedEvent("Clients");
            RaisePropertyChangedEvent("SelectedClient");
        }

        public void Refresh(IEnumerable<ClientDisplayModel> clients)
        {
            Clients.Clear();
            foreach (ClientDisplayModel client in clients)
            {
                Clients.Add(client);
            }

            RaisePropertyChangedEvent("SelectedClient");
        }

        public ICommand DeleteSelectedClientCommand { get; private set; }

        public ClientDisplayModel SelectedClient
        {
            get { return client; }
            set
            {
                client = value;
                RaisePropertyChangedEvent("SelectedClient");
            }
        }

        public ObservableCollection<ClientDisplayModel> Clients { get; private set; }

        private void RaiseClientDeletedEvent(ClientDisplayModel client)
        {
            var handler = ClientDeleted;
            if (handler != null)
            {
                ClientDisplayEventArgs e = new ClientDisplayEventArgs(client);
                handler(this, e);
            }
        }
    }
}
