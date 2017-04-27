using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IParticipantController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
