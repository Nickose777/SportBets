using SportBet.Services.Contracts;

namespace SportBet.Services.Providers
{
    public class SessionProvider : ISession
    {
        public string CurrentUserLogin
        {
            get
            {
                return Session.CurrentUserLogin;
            }
            set
            {
                Session.CurrentUserLogin = value;
            }
        }

        public string CurrentUserHashedPassword
        {
            get
            {
                return Session.CurrentUserHashedPassword;
            }
            set
            {
                Session.CurrentUserHashedPassword = value;
            }
        }
    }
}
