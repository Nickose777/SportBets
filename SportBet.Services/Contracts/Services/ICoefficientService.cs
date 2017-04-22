using System;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface ICoefficientService : IDisposable
    {
        ServiceMessage Create(CoefficientCreateDTO coefficientCreateDTO);
    }
}
