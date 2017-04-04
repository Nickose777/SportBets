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
            CreateAnalyticCommand = new DelegateCommand(() => RaiseAnalyticCreatedEvent(analytic), obj => CanCreateModel());
        }

        public ICommand CreateAnalyticCommand { get; private set; }

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
