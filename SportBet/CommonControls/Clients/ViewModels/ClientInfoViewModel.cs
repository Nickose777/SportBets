using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public abstract class ClientInfoViewModel : ObservableObject
    {
        public event ClientEditEventHandler ClientEdited;

        private readonly ClientEditModel client;
        private readonly ClientEditModel clientForEdit;

        public ClientInfoViewModel(ClientEditModel client)
        {
            this.client = client;
            this.clientForEdit = new ClientEditModel
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                DateOfBirth = client.DateOfBirth
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseClientEditedEvent(clientForEdit), CanSave);
            this.UndoCommand = new DelegateCommand(Undo, obj => IsDirty());

            this.Bets = new ObservableCollection<BetDisplayModel>(client.Bets);
        }

        public abstract bool ShowBetHistory { get; }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string LastName
        {
            get { return clientForEdit.LastName; }
            set
            {
                clientForEdit.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }

        public string FirstName
        {
            get { return clientForEdit.FirstName; }
            set
            {
                clientForEdit.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }

        public string PhoneNumber
        {
            get { return clientForEdit.PhoneNumber; }
            set
            {
                clientForEdit.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        public DateTime DateOfBirth
        {
            get { return clientForEdit.DateOfBirth; }
            set
            {
                clientForEdit.DateOfBirth = value;
                RaisePropertyChangedEvent("DateOfBirth");
            }
        }

        public ObservableCollection<BetDisplayModel> Bets { get; private set; }

        private void Undo()
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
            PhoneNumber = client.PhoneNumber;
            DateOfBirth = client.DateOfBirth;
        }

        private bool CanSave(object parameter)
        {
            return
                IsDirty() &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber);
        }

        private bool IsDirty()
        {
            return
                client.FirstName != FirstName ||
                client.LastName != LastName ||
                client.PhoneNumber != PhoneNumber ||
                client.DateOfBirth != DateOfBirth;
        }

        private void RaiseClientEditedEvent(ClientEditModel client)
        {
            var handler = ClientEdited;
            if (handler != null)
            {
                ClientEditEventArgs e = new ClientEditEventArgs(client);
                handler(this, e);
            }
        }
    }
}
