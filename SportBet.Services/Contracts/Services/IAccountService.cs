using System;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IAccountService : IDisposable
    {
        ServiceMessage Register(ClientRegisterDTO clientRegisterDTO);

        ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO);

        ServiceMessage Register(AdminRegisterDTO adminRegisterDTO);

        ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO);
    }
}
