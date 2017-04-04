using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.ClientControls.ViewModels
{
    public class ClientInfoViewModel : ObservableObject
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
        }

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
