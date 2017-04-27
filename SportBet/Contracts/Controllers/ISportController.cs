using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface ISportController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
