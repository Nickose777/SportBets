namespace SportBet.Contracts.Controllers
{
    public interface ICountryController : IReceiveMessage
    {
        void Add();

        void Display();
    }
}
