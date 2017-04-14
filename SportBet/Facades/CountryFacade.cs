using SportBet.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;

namespace SportBet.Facades
{
    class CountryFacade : FacadeBase<string>
    {
        public CountryFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<string> GetAll()
        {
            IEnumerable<string> countryNames = null;

            using (ICountryService service = factory.CreateCountryService())
            {
                DataServiceMessage<IEnumerable<string>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                countryNames = serviceMessage.IsSuccessful ? serviceMessage.Data : new List<string>();
            }

            return countryNames;
        }
    }
}
