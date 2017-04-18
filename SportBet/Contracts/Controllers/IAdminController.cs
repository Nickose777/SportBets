namespace SportBet.Contracts.Controllers
{
    public interface IAdminController : IReceiveMessage
    {
        void Register();

        void Display();
    }
}
