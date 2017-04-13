using System;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels;

namespace SportBet.Services.Contracts.Services
{
    public interface IAccountService : IDisposable
    {
        ServiceMessage Register(ClientRegisterDTO clientRegisterDTO);

        ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO);

        ServiceMessage Register(AdminRegisterDTO adminRegisterDTO);

        ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO);

        ServiceMessage ChangePassword(ChangePasswordDTO changePasswordDTO);
    }
}
