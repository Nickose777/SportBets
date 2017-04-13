﻿using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ClientInfoViewModel : ObservableObject
    {
        public event ClientEditEventHandler ClientEdited;

        private bool isEditMode;

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

            IsEditMode = false;

            StartEditClientCommand = new DelegateCommand(() => StartEdit(), obj => !isEditMode);
            CancelEditClientCommand = new DelegateCommand(() => CancelEdit(), obj => isEditMode);
            SaveClientCommand = new DelegateCommand(() => Save(), CanSave);
        }

        public ICommand StartEditClientCommand { get; private set; }
        private void StartEdit()
        {
            IsEditMode = true;
        }

        public ICommand CancelEditClientCommand { get; private set; }
        private void CancelEdit()
        {
            IsEditMode = false;
            FirstName = client.FirstName;
            LastName = client.LastName;
            PhoneNumber = client.PhoneNumber;
            DateOfBirth = client.DateOfBirth;
        }

        public ICommand SaveClientCommand { get; private set; }
        private void Save()
        {
            RaiseClientEditedEvent(clientForEdit);
            IsEditMode = false;
        }
        private bool CanSave(object parameter)
        {
            return
                isEditMode &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber) &&
                DateOfBirth != null;
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

        public bool IsEditMode
        {
            get { return isEditMode; }
            private set 
            { 
                isEditMode = value;
                RaisePropertyChangedEvent("IsEditMode");
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