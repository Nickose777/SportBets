using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IEventService : IDisposable
    {
        ServiceMessage Create(EventCreateDTO eventCreateDTO);
    }
}
