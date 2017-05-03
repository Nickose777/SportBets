using System;
using System.Windows.Input;

namespace SportBet.CommonControls.Settings.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        public event EventHandler SettingsChanged;

        public SettingsViewModel(bool showMessages)
        {
            ShowMessages = showMessages;

            this.SaveChangesCommand = new DelegateCommand(RaiseSettingsChangedEvent, obj => true);
        }

        public ICommand SaveChangesCommand { get; private set; }

        public bool ShowMessages { get; set; }

        private void RaiseSettingsChangedEvent()
        {
            var handler = SettingsChanged;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
