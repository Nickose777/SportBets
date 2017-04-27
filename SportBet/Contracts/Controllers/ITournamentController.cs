using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface ITournamentController : IReceiveMessage
    {
        void Add();

        void Display();

        UIElement GetAddElement();

        UIElement GetDisplayElement();
    }
}
