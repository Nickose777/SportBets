using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.AccountServices
{
    public class ClientAccountService : IAccountService
    {
        public AuthResult Register(ClientRegisterDTO clientRegisterDTO)
        {
            return new AuthResult("No permissions to register clients", false);
        }
        public AuthResult Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            return new AuthResult("No permissions to register bookmaker", false);
        }
    }
}
