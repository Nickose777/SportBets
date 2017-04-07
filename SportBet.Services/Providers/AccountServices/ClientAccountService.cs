using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.AccountServices
{
    class ClientAccountService : IAccountService
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

        public ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO)
        {
            return new ServiceMessage("No permissions to register analytic", false);
        }

        public void Dispose()
        {

        }
    }
}
