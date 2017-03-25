﻿using SportBet.Services.DTOModels;
using SportBet.Services.Factories;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts
{
    public interface IAuthService : IDisposable
    {
        AuthResult Register(ClientRegisterDTO clientRegisterDTO);
        IServiceFactory Login(UserLoginDTO userLoginDTO);
    }
}
