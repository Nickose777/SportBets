using System;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IAuthService : IDisposable
    {
        bool EstablishConnection();

        LoginServiceMessage Login(UserLoginDTO userLoginDTO);
    }
}
