namespace SportBet.Contracts.Controllers
{
    public interface ISportController : IReceiveMessage
    {
        void Add();

        void Display();
    }
}
