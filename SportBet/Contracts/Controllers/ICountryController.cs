using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface ICountryController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
