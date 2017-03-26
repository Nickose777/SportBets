using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.ResultTypes
{
    public class AuthServiceFactoryResult : AuthResult
    {
        public ServiceFactory Factory { get; private set; }
        public LoginType LoginType { get; private set; }

        public AuthServiceFactoryResult(ServiceFactory factory, LoginType loginType, string message, bool success)
            : base(message, success)
        {
            this.Factory = factory;
            this.LoginType = loginType;
        }
    }
}
