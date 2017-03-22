using SportBet.Controllers.DTOModels;
using SportBet.Controllers.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Controllers.Contracts
{
    public interface IAuthService
    {
        AuthResult Register(ClientRegisterDTO clientRegisterDTO);
        AuthResult LoginAsSuperUser(UserLoginDTO adminLoginDTO);
        AuthResult LoginAsAdmin(UserLoginDTO adminLoginDTO);
        AuthResult LoginAsAnalyst(UserLoginDTO adminLoginDTO);
        AuthResult LoginAsBookmaker(UserLoginDTO clientLoginDTO);
        AuthResult LoginAsClient(UserLoginDTO clientLoginDTO);
    }
}
