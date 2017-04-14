using System.Collections.Generic;
using SportBet.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;

namespace SportBet.Facades
{
    class SportFacade : FacadeBase<string>
    {
        public SportFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<string> GetAll()
        {
            IEnumerable<string> sportNames = null;

            using (ISportService service = factory.CreateSportService())
            {
                DataServiceMessage<IEnumerable<string>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                sportNames = serviceMessage.IsSuccessful ? serviceMessage.Data : new List<string>();
            }

            return sportNames;
        }
    }
}
