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
    public class AnalyticRegisterViewModel : RegisterObservableObject
    {
        public event AnalyticRegisterEventHandler AnalyticCreated;

        private readonly AnalyticRegisterModel analytic;

        public AnalyticRegisterViewModel(AnalyticRegisterModel model)
            : base(model)
        {
            analytic = model;
            CreateAnalyticCommand = new DelegateCommand(() => RaiseAnalyticCreatedEvent(analytic), CanCreateAnalytic);
        }

        public ICommand CreateAnalyticCommand { get; private set; }
        private bool CanCreateAnalytic(object obj)
        {
            return
                CanCreateModel() &&
                !String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(PhoneNumber);
        }

        public string LastName
        {
            get { return analytic.LastName; }
            set
            {
                analytic.LastName = value;
                RaisePropertyChangedEvent("LastName");
            }
        }
        public string FirstName
        {
            get { return analytic.FirstName; }
            set
            {
                analytic.FirstName = value;
                RaisePropertyChangedEvent("FirstName");
            }
        }
        public string PhoneNumber
        {
            get { return analytic.PhoneNumber; }
            set
            {
                analytic.PhoneNumber = value;
                RaisePropertyChangedEvent("PhoneNumber");
            }
        }

        private void RaiseAnalyticCreatedEvent(AnalyticRegisterModel analytic)
        {
            var handler = AnalyticCreated;
            if (handler != null)
            {
                AnalyticEventArgs e = new AnalyticEventArgs(analytic);
                handler(this, e);
            }
        }
    }
}
