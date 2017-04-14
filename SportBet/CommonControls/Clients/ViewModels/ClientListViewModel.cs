using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ClientListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<ClientDisplayModel> facade;

        private ClientDisplayModel client;
        private string firstNameFilter;
        private string lastNameFilter;
        private string phoneNumberFilter;

        public ClientListViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            subject.Subscribe(this);

            this.Clients = new ObservableCollection<ClientDisplayModel>(facade.GetAll());
            this.SortedClients.Filter = FilterClient;
            this.RefreshClients = new DelegateCommand(() => SortedClients.Refresh(), obj => true);

            RaisePropertyChangedEvent("Clients");
            RaisePropertyChangedEvent("SelectedClient");
        }

        public ICommand RefreshClients { get; private set; }

        private bool FilterClient(object obj)
        {
            ClientDisplayModel client = obj as ClientDisplayModel;

            return
                client.FirstName.Contains(firstNameFilter ?? String.Empty) &&
                client.LastName.Contains(lastNameFilter ?? String.Empty) &&
                client.PhoneNumber.Contains(phoneNumberFilter ?? String.Empty);
        }

        public string FirstNameFilter
        {
            get { return firstNameFilter; }
            set
            {
                firstNameFilter = value;
                RaisePropertyChangedEvent("FirstNameFilter");
            }
        }
        public string LastNameFilter
        {
            get { return lastNameFilter; }
            set
            {
                lastNameFilter = value;
                RaisePropertyChangedEvent("LastNameFilter");
            }
        }
        public string PhoneNumberFilter
        {
            get { return phoneNumberFilter; }
            set
            {
                phoneNumberFilter = value;
                RaisePropertyChangedEvent("PhoneNumberFilter");
            }
        }

        public void Update()
        {
            IEnumerable<ClientDisplayModel> clients = facade.GetAll();

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

        public ICollectionView SortedClients
        {
            get { return CollectionViewSource.GetDefaultView(Clients); }
        }

        public ObservableCollection<ClientDisplayModel> Clients { get; private set; }
    }
}
