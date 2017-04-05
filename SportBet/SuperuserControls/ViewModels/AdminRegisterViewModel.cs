using SportBet.EventHandlers.Register;
using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.SuperuserControls.ViewModels
{
    public class AdminRegisterViewModel : RegisterObservableObject
    {
        public event AdminRegisterEventHandler AdminCreated;

        private readonly AdminRegisterModel admin;

        public AdminRegisterViewModel(AdminRegisterModel model)
            : base(model)
        {
            admin = model;
            CreateAdminCommand = new DelegateCommand(() => RaiseAdminCreatedEvent(admin), CanCreateAdmin);
        }

        public ICommand CreateAdminCommand { get; private set; }
        private bool CanCreateAdmin(object obj)
        {
            return
                CanCreateModel() &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber);
        }

        public string LastName
        {
            get { return admin.LastName; }
            set
            {
                admin.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }
        public string FirstName
        {
            get { return admin.FirstName; }
            set
            {
                admin.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }
        public string PhoneNumber
        {
            get { return admin.PhoneNumber; }
            set
            {
                admin.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void RaiseAdminCreatedEvent(AdminRegisterModel admin)
        {
            var handler = AdminCreated;
            if (handler != null)
            {
                AdminEventArgs e = new AdminEventArgs(admin);
                handler(this, e);
            }
        }
    }
}
