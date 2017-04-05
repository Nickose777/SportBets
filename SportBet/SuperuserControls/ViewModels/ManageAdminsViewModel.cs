using SportBet.EventHandlers;
using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageAdminsViewModel : ObservableObject
    {
        public event AdminDisplayEventHandler AdminDeleted;

        private AdminDisplayModel admin;

        public ManageAdminsViewModel(IEnumerable<AdminDisplayModel> admins)
        {
            this.Admins = new ObservableCollection<AdminDisplayModel>(admins);

            this.DeleteSelectedAdminCommand = new DelegateCommand(
                () => RaiseAdminDeletedEvent(SelectedAdmin),
                obj => SelectedAdmin != null);

            RaisePropertyChangedEvent("Admins");
            RaisePropertyChangedEvent("SelectedAdmin");
        }

        public void Refresh(IEnumerable<AdminDisplayModel> admins)
        {
            Admins.Clear();
            foreach (AdminDisplayModel admin in admins)
            {
                Admins.Add(admin);
            }

            RaisePropertyChangedEvent("SelectedAdmin");
        }

        public ICommand DeleteSelectedAdminCommand { get; private set; }

        public AdminDisplayModel SelectedAdmin
        {
            get { return admin; }
            set
            {
                admin = value;
                RaisePropertyChangedEvent("SelectedAdmin");
            }
        }

        public ObservableCollection<AdminDisplayModel> Admins { get; private set; }

        private void RaiseAdminDeletedEvent(AdminDisplayModel admin)
        {
            var handler = AdminDeleted;
            if (handler != null)
            {
                AdminDisplayEventArgs e = new AdminDisplayEventArgs(admin);
                handler(this, e);
            }
        }
    }
}
