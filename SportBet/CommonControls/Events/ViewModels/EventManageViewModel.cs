using System.Collections.Generic;
using SportBet.Models.Base;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventManageViewModel : ObservableObject
    {
        public EventInfoViewModel InfoViewModel { get; private set; }

        public EventParticipantViewModel EventParticipantViewModel { get; private set; }

        public EventManageViewModel(EventDisplayModel eventDisplayModel, IEnumerable<ParticipantBaseModel> allParticipants)
        {
            InfoViewModel = new EventInfoViewModel(eventDisplayModel);
            EventParticipantViewModel = new EventParticipantViewModel(eventDisplayModel, allParticipants);
        }
    }
}
