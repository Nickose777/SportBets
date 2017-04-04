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
            CreateAdminCommand = new DelegateCommand(() => RaiseAdminCreatedEvent(admin), obj => CanCreateModel());
        }

        public ICommand CreateAdminCommand { get; private set; }

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
