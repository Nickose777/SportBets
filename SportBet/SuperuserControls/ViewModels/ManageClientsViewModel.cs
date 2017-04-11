using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageClientsViewModel : ObservableObject, IClientObserver
    {
        public event ClientDisplayEventHandler ClientDeleteRequest;

        private readonly IClientSubject subject;
        private readonly IClientController controller;
        private ClientDisplayModel client;

        public ManageClientsViewModel(IClientSubject subject, IClientController controller)
        {
            this.subject = subject;
            this.controller = controller;

            subject.Subscribe(this);

            this.Clients = new ObservableCollection<ClientDisplayModel>(controller.GetAllNotDeleted());

            this.DeleteSelectedClientCommand = new DelegateCommand(
                () => RaiseClientDeleteRequestEvent(SelectedClient),
                obj => SelectedClient != null);

            RaisePropertyChangedEvent("Clients");
            RaisePropertyChangedEvent("SelectedClient");
        }

        public ICommand DeleteSelectedClientCommand { get; private set; }

        public void Update()
        {
            IEnumerable<ClientDisplayModel> clients = controller.GetAllNotDeleted();

            Clients.Clear();
            foreach (ClientDisplayModel clientDisplayModel in clients)
            {
                Clients.Add(clientDisplayModel);
            }

            RaisePropertyChangedEvent("SelectedClient");
        }

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
