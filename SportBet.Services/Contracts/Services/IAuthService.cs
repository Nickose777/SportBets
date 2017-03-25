using SportBet.Services.Contracts.Factories;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Services
{
    public interface IAuthService : IDisposable
    {
        AuthResult Register(ClientRegisterDTO clientRegisterDTO);
        AuthServiceFactoryResult Login(UserLoginDTO userLoginDTO);
    }
}
