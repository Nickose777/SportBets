using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.CommonControls.Coefficients.ViewModels
{
    public class CoefficientListViewModel : ObservableObject, IObserver
    {
        private readonly FacadeBase<CoefficientDisplayModel> facade;

        private CoefficientDisplayModel selectedCoefficient;

        public CoefficientListViewModel(ISubject subject, FacadeBase<CoefficientDisplayModel> facade)
        {
            this.facade = facade;

            this.Coefficients = new ObservableCollection<CoefficientDisplayModel>(facade.GetAll());

            subject.Subscribe(this);
        }

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
    }
}
