using System.Collections.Generic;
using SportBet.Contracts.Controllers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;

namespace SportBet.Controllers
{
    class CountryController : BaseController, ICountryController
    {
        private readonly ServiceFactory factory;

        public CountryController(ServiceFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<string> GetAll()
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
