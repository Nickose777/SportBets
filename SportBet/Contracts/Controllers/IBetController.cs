using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IBetController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
