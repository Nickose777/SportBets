using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ClientListViewModel : ObservableObject, IClientObserver
    {
        private readonly IClientSubject subject;
        private readonly IClientController controller;
        private ClientDisplayModel client;

        public ClientListViewModel(IClientSubject subject, IClientController controller)
        {
            this.subject = subject;
            this.controller = controller;

            subject.Subscribe(this);

            this.Clients = new ObservableCollection<ClientDisplayModel>(controller.GetAllNotDeleted());

            RaisePropertyChangedEvent("Clients");
            RaisePropertyChangedEvent("SelectedClient");
        }

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
    }
}
