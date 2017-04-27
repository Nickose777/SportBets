using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SportBet.CommonControls.Errors
{
    public class ErrorViewModel : ObservableObject
    {
        private ServiceMessage selectedMessage;

        public ErrorViewModel(IEnumerable<ServiceMessage> messages)
        {
            Messages = new ObservableCollection<ServiceMessage>(messages);
        }

        public ServiceMessage SelectedMessage
        {
            get { return selectedMessage; }
            set
            {
                selectedMessage = value;
                RaisePropertyChangedEvent("SelectedMessage");
            }
        }

        public ObservableCollection<ServiceMessage> Messages { get; private set; }
    }
}
