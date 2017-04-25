namespace SportBet.Contracts.Controllers
{
    public interface IBetController : IReceiveMessage
    {
        void Create();
    }
}
