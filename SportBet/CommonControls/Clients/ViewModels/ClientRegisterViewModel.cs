using System;
using System.Windows.Input;
using SportBet.EventHandlers.Register;
using SportBet.Models.Registers;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ClientRegisterViewModel : RegisterObservableObject
    {
        public event ClientRegisterEventHandler ClientCreated;

        private readonly ClientRegisterModel client;

        public ClientRegisterViewModel(ClientRegisterModel client)
            : base(client)
        {
            this.client = client;
            CreateClientCommand = new DelegateCommand(() => RaiseClientCreatedEvent(client), CanCreateBookmaker);
        }

        public ICommand CreateClientCommand { get; private set; }

        public string LastName
        {
            get { return client.LastName; }
            set
            {
                client.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }

        public string FirstName
        {
            get { return client.FirstName; }
            set
            {
                client.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }

        public string PhoneNumber
        {
            get { return client.PhoneNumber; }
            set
            {
                client.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        public DateTime DateOfBirth
        {
            get { return client.DateOfBirth; }
            set
            {
                client.DateOfBirth = value;
                RaisePropertyChangedEvent("DateOfBirth");
            }
        }

        private bool CanCreateBookmaker(object obj)
        {
            return
                CanCreateModel() &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber) &&
                DateOfBirth != null;
        }

        private void RaiseClientCreatedEvent(ClientRegisterModel client)
        {
            var handler = ClientCreated;
            if (handler != null)
            {
                ClientEventArgs e = new ClientEventArgs(client);
                handler(this, e);
            }
        }
    }
}
