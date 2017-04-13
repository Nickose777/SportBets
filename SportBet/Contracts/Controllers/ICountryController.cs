using System.Collections.Generic;

namespace SportBet.Contracts.Controllers
{
    public interface ICountryController : IController
    {
        IEnumerable<string> GetAll();
    }
}
