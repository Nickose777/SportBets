using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ICoefficientService : IDisposable
    {
        ServiceMessage Create(CoefficientCreateDTO coefficientCreateDTO);

        ServiceMessage Update(CoefficientEditDTO coefficientEditDTO);

        DataServiceMessage<IEnumerable<CoefficientDisplayDTO>> GetAll();
    }
}
