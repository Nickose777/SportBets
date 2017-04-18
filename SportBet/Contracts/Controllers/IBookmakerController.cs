namespace SportBet.Contracts.Controllers
{
    public interface IBookmakerController : IReceiveMessage
    {
        void Register();

        void Display();
    }
}
