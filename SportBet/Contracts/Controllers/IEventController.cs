using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IEventController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
