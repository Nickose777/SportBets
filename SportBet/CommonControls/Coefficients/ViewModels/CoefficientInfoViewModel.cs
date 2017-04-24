using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Coefficients.ViewModels
{
    public class CoefficientInfoViewModel : ObservableObject
    {
        public event CoefficientEditEventHandler CoefficientEdited;

        private readonly CoefficientEditModel coefficientEditModel;

        public CoefficientInfoViewModel(CoefficientDisplayModel coefficient)
        {
            this.coefficientEditModel = new CoefficientEditModel
            {
                SportName = coefficient.SportName,
                TournamentName = coefficient.TournamentName,
                DateOfEvent = coefficient.DateOfEvent,

                Participants = coefficient.Participants,

                Value = coefficient.Value,
                NewValue = coefficient.Value,

                Description = coefficient.Description,
                NewDescription = coefficient.Description
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseCoefficientEditedEvent(coefficientEditModel), CanSaveChanges);
            this.UndoCommand = new DelegateCommand(Undo, IsDirty);

            this.Participants = new ObservableCollection<ParticipantBaseModel>(coefficientEditModel.Participants);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string SportName
        {
            get { return coefficientEditModel.SportName; }
        }

        public string TournamentName
        {
            get { return coefficientEditModel.TournamentName; }
        }

        public DateTime DateOfEvent
        {
            get { return coefficientEditModel.DateOfEvent; }
        }

        public decimal CoefficientValue
        {
            get { return coefficientEditModel.NewValue; }
            set
            {
                coefficientEditModel.NewValue = value;
                RaisePropertyChangedEvent("CoefficientValue");
            }
        }

        public string Description
        {
            get { return coefficientEditModel.NewDescription; }
            set
            {
                coefficientEditModel.NewDescription = value;
                RaisePropertyChangedEvent("Description");
            }
        }

        public ObservableCollection<ParticipantBaseModel> Participants { get; private set; }

        private bool CanSaveChanges(object parameter)
        {
            return
                IsDirty(parameter) &&
                CoefficientValue > 0 &&
                !String.IsNullOrEmpty(Description) &&
                Description.Length <= 100;
        }

        private bool IsDirty(object parameter)
        {
            return
                coefficientEditModel.Value != coefficientEditModel.NewValue ||
                coefficientEditModel.Description != coefficientEditModel.NewDescription;

        }

        private void Undo()
        {
            CoefficientValue = coefficientEditModel.Value;
            Description = coefficientEditModel.Description;
        }

        private void RaiseCoefficientEditedEvent(CoefficientEditModel coefficient)
        {
            var handler = CoefficientEdited;
            if (handler != null)
            {
                CoefficientEditEventArgs e = new CoefficientEditEventArgs(coefficient);
                handler(this, e);
            }
        }
    }
}
