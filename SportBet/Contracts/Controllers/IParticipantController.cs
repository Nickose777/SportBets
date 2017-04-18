namespace SportBet.Contracts.Controllers
{
    public interface IParticipantController : IReceiveMessage
    {
        void Create();

        void Display();
    }
}
