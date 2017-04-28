using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IAdminController : IReceiveMessage
    {
        void Register();

        void Display();

        UIElement GetRegisterElement();

        UIElement GetDisplayElement();
    }
}
