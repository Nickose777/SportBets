using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IClientController : IReceiveMessage
    {
        void Register();

        void Display();

        UIElement GetRegisterElement();

        UIElement GetDisplayElement();
    }
}
