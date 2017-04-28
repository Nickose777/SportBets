using System;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.SuperuserControls.ViewModels
{
    public class AdminInfoViewModel : ObservableObject
    {
        public event AdminEditEventHandler AdminEdited;

        private readonly AdminEditModel admin;
        private readonly AdminEditModel adminForEdit;

        public AdminInfoViewModel(AdminEditModel admin)
        {
            this.admin = admin;
            this.adminForEdit = new AdminEditModel
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                Login = admin.Login
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseAdminEditedEvent(adminForEdit), CanSave);
            this.UndoCommand = new DelegateCommand(Undo, obj => IsDirty());
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string LastName
        {
            get { return adminForEdit.LastName; }
            set
            {
                adminForEdit.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }

        public string FirstName
        {
            get { return adminForEdit.FirstName; }
            set
            {
                adminForEdit.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }

        public string PhoneNumber
        {
            get { return adminForEdit.PhoneNumber; }
            set
            {
                adminForEdit.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void Undo()
        {
            FirstName = admin.FirstName;
            LastName = admin.LastName;
            PhoneNumber = admin.PhoneNumber;
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
                admin.FirstName != FirstName ||
                admin.LastName != LastName ||
                admin.PhoneNumber != PhoneNumber;
        }

        private void RaiseAdminEditedEvent(AdminEditModel admin)
        {
            var handler = AdminEdited;
            if (handler != null)
            {
                AdminEditEventArgs e = new AdminEditEventArgs(admin);
                handler(this, e);
            }
        }
    }
}
