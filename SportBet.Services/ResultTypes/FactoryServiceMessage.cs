using SportBet.Services.Contracts.Factories;

namespace SportBet.Services.ResultTypes
{
    public class FactoryServiceMessage : ServiceMessage
    {
        public ServiceFactory Factory { get; private set; }

        public LoginType LoginType { get; private set; }

        public FactoryServiceMessage(ServiceFactory factory, LoginType loginType, string message, bool success)
            : base(message, success)
        {
            this.Factory = factory;
            this.LoginType = loginType;
        }
    }
}
