using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IAnalyticController : IReceiveMessage
    {
        void Register();

        void Display();

        UIElement GetRegisterElement();

        UIElement GetDisplayElement();
    }
}
