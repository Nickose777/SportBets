using System.Collections.Generic;

namespace SportBet.Contracts.Controllers
{
    public interface ISportController : IController
    {
        IEnumerable<string> GetAll();
    }
}
