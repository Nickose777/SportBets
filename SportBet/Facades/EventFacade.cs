using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using System.Collections.Generic;

namespace SportBet.Facades
{
    class EventFacade : FacadeBase<EventDisplayModel>
    {
        public EventFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<EventDisplayModel> GetAll()
        {
            return null;
        }
    }
}
