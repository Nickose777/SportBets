using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface ICoefficientController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
