using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface IBookmakerService : IDisposable
    {
        ServiceMessage Update(BookmakerEditDTO bookmakerEditDTO, string login);

        ServiceMessage Delete(string login);

        DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> GetAll();
    }
}
