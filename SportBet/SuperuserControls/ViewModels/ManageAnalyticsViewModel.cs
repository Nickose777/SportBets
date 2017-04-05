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
    public class ManageAnalyticsViewModel : ObservableObject
    {
        public event AnalyticDisplayEventHandler AnalyticDeleted;

        private AnalyticDisplayModel analytic;

        public ManageAnalyticsViewModel(IEnumerable<AnalyticDisplayModel> analytics)
        {
            this.Analytics = new ObservableCollection<AnalyticDisplayModel>(analytics);

            this.DeleteSelectedAnalyticCommand = new DelegateCommand(
                () => RaiseAnalyticDeletedEvent(SelectedAnalytic),
                obj => SelectedAnalytic != null);

            RaisePropertyChangedEvent("Analytics");
            RaisePropertyChangedEvent("SelectedAnalytic");
        }

        public void Refresh(IEnumerable<AnalyticDisplayModel> analytics)
        {
            Analytics.Clear();
            foreach (AnalyticDisplayModel analytic in analytics)
            {
                Analytics.Add(analytic);
            }

            RaisePropertyChangedEvent("SelectedAnalytic");
        }

        public ICommand DeleteSelectedAnalyticCommand { get; private set; }

        public AnalyticDisplayModel SelectedAnalytic
        {
            get { return analytic; }
            set
            {
                analytic = value;
                RaisePropertyChangedEvent("SelectedAnalytic");
            }
        }

        public ObservableCollection<AnalyticDisplayModel> Analytics { get; private set; }

        private void RaiseAnalyticDeletedEvent(AnalyticDisplayModel analytic)
        {
            var handler = AnalyticDeleted;
            if (handler != null)
            {
                AnalyticDisplayEventArgs e = new AnalyticDisplayEventArgs(analytic);
                handler(this, e);
            }
        }
    }
}
