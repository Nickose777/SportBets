namespace SportBet.Contracts.Controllers
{
    public interface IEventController : IReceiveMessage
    {
        void Create();

        void Display();
    }
}
