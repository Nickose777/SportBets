using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageAdminsViewModel : ObservableObject, IObserver
    {
        public event AdminDisplayEventHandler AdminDeleted;

        private readonly ISubject subject;
        private readonly FacadeBase<AdminDisplayModel> facade;

        private AdminDisplayModel admin;

        public ManageAdminsViewModel(ISubject subject, FacadeBase<AdminDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Admins = new ObservableCollection<AdminDisplayModel>(facade.GetAll());

            this.DeleteSelectedAdminCommand = new DelegateCommand(
                () => RaiseAdminDeletedEvent(SelectedAdmin),
                obj => SelectedAdmin != null);

            subject.Subscribe(this);

            RaisePropertyChangedEvent("Admins");
            RaisePropertyChangedEvent("SelectedAdmin");
        }

        public void Update()
        {
            IEnumerable<AdminDisplayModel> admins = facade.GetAll();

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
