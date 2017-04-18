namespace SportBet.Contracts.Controllers
{
    public interface IAnalyticController : IReceiveMessage
    {
        void Register();

        void Display();
    }
}
