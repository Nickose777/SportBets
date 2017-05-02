using System;
using System.Windows.Input;
using SportBet.Facades;
using SportBet.Services.DTOModels.Display;

namespace SportBet.AnalyticControls.ViewModels
{
    public class IncomeViewModel : ObservableObject
    {
        private readonly AnalysisFacade facade;
        private IncomeDTO income;

        public IncomeViewModel(AnalysisFacade facade)
        {
            this.facade = facade;
            this.income = new IncomeDTO();

            this.ApplyCommand = new DelegateCommand(Apply, obj => true);

            StartDate = new DateTime(DateTime.Now.Year, 01, 01);
            EndDate = new DateTime(DateTime.Now.Year + 1, 01, 01);
        }

        public ICommand ApplyCommand { get; private set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WonBetsCount
        {
            get { return income.WonBets; }
        }

        public int LostBetsCount
        {
            get { return income.LostBets; }
        }

        public decimal Profits
        {
            get { return income.Profits; }
        }

        public decimal Costs
        {
            get { return income.Costs; }
        }

        public decimal Income
        {
            get { return income.Income; }
        }

        private void Apply()
        {
            income = facade.GetIncome(StartDate, EndDate);
            RaisePropertyChangedEvent("WonBetsCount");
            RaisePropertyChangedEvent("LostBetsCount");

            RaisePropertyChangedEvent("Profits");
            RaisePropertyChangedEvent("Costs");
            RaisePropertyChangedEvent("Income");
        }
    }
}
