using System.Collections.Generic;
using SportBet.Models.Base;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventManageViewModel : ObservableObject
    {
        public EventInfoViewModel InfoViewModel { get; private set; }

        public EventManageViewModel(EventDisplayModel eventDisplayModel)
        {
            InfoViewModel = new EventInfoViewModel(eventDisplayModel);
        }
    }
}
