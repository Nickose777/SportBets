using System.Windows;
using System.Windows.Controls;
namespace SportBet.Contracts.Controllers
{
    public interface IAccountController : IReceiveMessage
    {
        void ChangePassword();

        UIElement GetPasswordElement();

        UIElement GetAccountElement();
    }
}
