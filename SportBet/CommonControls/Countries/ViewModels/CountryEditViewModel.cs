using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Countries.ViewModels
{
    public class CountryEditViewModel : ObservableObject
    {
        public event CountryEditEventHandler CountryEdited;

        private readonly CountryEditModel countryEditModel;

        public CountryEditViewModel(string oldCountryName)
        {
            countryEditModel = new CountryEditModel
            {
                OldCountryName = oldCountryName,
                NewCountryName = oldCountryName
            };

            this.SaveChangesCommand = new DelegateCommand(() => RaiseCountryEditedEvent(countryEditModel), CanSaveChanges);
            this.UndoCommand = new DelegateCommand(() => CountryName = countryEditModel.OldCountryName, obj => true);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string CountryName
        {
            get { return countryEditModel.NewCountryName; }
            set
            {
                countryEditModel.NewCountryName = value;
                RaisePropertyChangedEvent("CountryName");
            }
        }

        private bool CanSaveChanges(object parameter)
        {
            return 3 < CountryName.Length && CountryName.Length <= 20;
        }

        private void RaiseCountryEditedEvent(CountryEditModel countryEditModel)
        {
            var handler = CountryEdited;
            if (handler != null)
            {
                CountryEditEventArgs e = new CountryEditEventArgs(countryEditModel);
                handler(this, e);
            }
        }
    }
}
