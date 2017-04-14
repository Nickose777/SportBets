using SportBet.Contracts.Controllers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Controllers
{
    class SportController : BaseController, ISportController
    {
        private readonly ServiceFactory factory;

        public SportController(ServiceFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<string> GetAll()
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
