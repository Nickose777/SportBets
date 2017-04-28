using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.SuperuserControls.ViewModels
{
    public class ManageAnalyticsViewModel : ObservableObject, IObserver
    {
        public event AnalyticDisplayEventHandler AnalyticDeleted;

        private readonly ISubject subject;
        private readonly FacadeBase<AnalyticDisplayModel> facade;

        private AnalyticDisplayModel analytic;

        public ManageAnalyticsViewModel(ISubject subject, FacadeBase<AnalyticDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            this.Analytics = new ObservableCollection<AnalyticDisplayModel>(facade.GetAll());

            this.DeleteSelectedAnalyticCommand = new DelegateCommand(
                () => RaiseAnalyticDeletedEvent(SelectedAnalytic),
                obj => SelectedAnalytic != null);

            subject.Subscribe(this);
            
            RaisePropertyChangedEvent("Analytics");
            RaisePropertyChangedEvent("SelectedAnalytic");
        }

        public void Update()
        {
            IEnumerable<AnalyticDisplayModel> analytics = facade.GetAll();

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
