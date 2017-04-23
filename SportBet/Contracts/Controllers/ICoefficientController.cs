namespace SportBet.Contracts.Controllers
{
    public interface ICoefficientController : IReceiveMessage
    {
        void Create();

        void Display();
    }
}
