using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.AdminControls.ViewModels
{
    public class SportEditViewModel : ObservableObject
    {
        public event SportEditEventHandler SportEdited;

        private readonly SportEditModel sportEditModel;

        public SportEditViewModel(string oldSportName)
        {
            sportEditModel = new SportEditModel
            {
                OldSportName = oldSportName,
                NewSportName = oldSportName
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseSportEditedEvent(sportEditModel), CanSaveChanges);
            this.UndoCommand = new DelegateCommand(() => SportName = sportEditModel.OldSportName, obj => true);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string SportName
        {
            get { return sportEditModel.NewSportName; }
            set
            {
                sportEditModel.NewSportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        private bool CanSaveChanges(object parameter)
        {
            return 3 < SportName.Length && SportName.Length <= 20;
        }

        private void RaiseSportEditedEvent(SportEditModel sportEditModel)
        {
            var handler = SportEdited;
            if (handler != null)
            {
                SportEditEventArgs e = new SportEditEventArgs(sportEditModel);
                handler(this, e);
            }
        }
    }
}
