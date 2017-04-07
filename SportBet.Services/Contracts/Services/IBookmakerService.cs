using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IBookmakerService : IDisposable
    {
        ServiceMessage Delete(BookmakerDisplayDTO bookmaker);

        DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> GetAll();
    }
}
