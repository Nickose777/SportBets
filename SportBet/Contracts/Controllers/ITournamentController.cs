namespace SportBet.Contracts.Controllers
{
    public interface ITournamentController : IReceiveMessage
    {
        void Create();
    }
}
