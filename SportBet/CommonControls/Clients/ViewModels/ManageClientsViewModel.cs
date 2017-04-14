﻿using System.Windows.Input;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;
using SportBet.Contracts;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ManageClientsViewModel : ObservableObject
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

        public ManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade, bool allowDeleteClient)
        {
            ClientListViewModel = new ClientListViewModel(subject, facade);
            CanDeleteClient = allowDeleteClient;

            this.SelectClientCommand = new DelegateCommand(
                () => RaiseClientSelectRequestEvent(SelectedClient),
                obj => SelectedClient != null);
            this.DeleteClientCommand = new DelegateCommand(
                () => RaiseClientDeleteRequestEvent(SelectedClient),
                obj => SelectedClient != null);
        }

        public ICommand SelectClientCommand { get; private set; }
        public ICommand DeleteClientCommand { get; private set; }

        public bool CanDeleteClient { get; private set; }

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
