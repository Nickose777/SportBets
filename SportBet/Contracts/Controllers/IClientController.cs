using System.Collections.Generic;
using SportBet.Models.Display;

namespace SportBet.Contracts.Controllers
{
    public interface IClientController : IController
    {
        IEnumerable<ClientDisplayModel> GetAllNotDeleted();
    }
}
