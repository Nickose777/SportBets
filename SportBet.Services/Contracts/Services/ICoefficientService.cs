using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Display;
using System.Collections.Generic;

namespace SportBet.Services.Contracts.Services
{
    public interface ICoefficientService : IDisposable
    {
        ServiceMessage Create(CoefficientCreateDTO coefficientCreateDTO);

        DataServiceMessage<IEnumerable<CoefficientDisplayDTO>> GetAll();
    }
}
