using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Services
{
    public interface IAccountService : IDisposable
    {
        ServiceMessage Register(ClientRegisterDTO clientRegisterDTO);
        ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO);
        ServiceMessage Register(AdminRegisterDTO adminRegisterDTO);
    }
}
