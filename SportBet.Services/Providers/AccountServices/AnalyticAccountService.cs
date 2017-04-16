using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;

namespace SportBet.Services.Providers.AccountServices
{
    public class AnalyticAccountService : AccountServiceBase, IAccountService
    {
        public AnalyticAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor, ISession session)
            : base(unitOfWork, registerValidator, encryptor, session) { }

        protected override void OnPasswordChange(string login, string newHashedPassword)
        {

        }
    }
}
