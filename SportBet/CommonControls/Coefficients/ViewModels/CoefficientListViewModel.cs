using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.EventHandlers.Display;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Coefficients.ViewModels
{
    public class CoefficientListViewModel : ObservableObject, IObserver
    {
        public event CoefficientDisplayEventHandler CoefficientSelected;

        private readonly FacadeBase<CoefficientDisplayModel> facade;

        private CoefficientDisplayModel selectedCoefficient;

        public CoefficientListViewModel(ISubject subject, FacadeBase<CoefficientDisplayModel> facade)
        {
            this.facade = facade;

            this.SelectCoefficientCommand = new DelegateCommand(
                () => RaiseCoefficientSelectedEvent(SelectedCoefficient), 
                obj => SelectedCoefficient != null);

            this.Coefficients = new ObservableCollection<CoefficientDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

        public ICommand SelectCoefficientCommand { get; private set; }

        public CoefficientDisplayModel SelectedCoefficient
        {
            get { return selectedCoefficient; }
            set
            {
                selectedCoefficient = value;
                RaisePropertyChangedEvent("SelectedCoefficient");
            }
        }

        public void Update()
        {
            IEnumerable<CoefficientDisplayModel> coefficientDisplayModels = facade.GetAll();

            Coefficients.Clear();
            foreach (CoefficientDisplayModel coefficientDisplayModel in coefficientDisplayModels)
            {
                Coefficients.Add(coefficientDisplayModel);
            }
        }

        public ObservableCollection<CoefficientDisplayModel> Coefficients { get; private set; }

        private void RaiseCoefficientSelectedEvent(CoefficientDisplayModel coefficient)
        {
            var handler = CoefficientSelected;
            if (handler != null)
            {
                CoefficientDisplayEventArgs e = new CoefficientDisplayEventArgs(coefficient);
                handler(this, e);
            }
        }
    }
}
