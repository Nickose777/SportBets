using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
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
        public ServiceMessage Register(ClientRegisterDTO clientRegisterDTO)
        {
            return new ServiceMessage("No permissions to register clients", false);
        }

        public ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            return new ServiceMessage("No permissions to register bookmaker", false);
        }

        public ServiceMessage Register(AdminRegisterDTO adminRegisterDTO)
        {
            return new ServiceMessage("No permissions to register admin", false);
        }

        public void Dispose()
        {

        }
    }
}
