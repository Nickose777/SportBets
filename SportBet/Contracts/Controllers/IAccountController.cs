namespace SportBet.Contracts.Controllers
{
    public interface IAccountController : IReceiveMessage
    {
        void ChangePassword();
    }
}
