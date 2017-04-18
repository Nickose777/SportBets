using SportBet.Services.Contracts;

namespace SportBet.Services.ResultTypes
{
    public class LoginServiceMessage : ServiceMessage
    {
        public ServiceFactory Factory { get; private set; }

        public LoginType LoginType { get; private set; }

        public LoginServiceMessage(ServiceFactory factory, LoginType loginType, string message, bool success)
            : base(message, success)
        {
            this.Factory = factory;
            this.LoginType = loginType;
        }
    }
}
