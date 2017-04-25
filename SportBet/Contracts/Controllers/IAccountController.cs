using System.Windows.Controls;
namespace SportBet.Contracts.Controllers
{
    public interface IAccountController : IReceiveMessage
    {
        void ChangePassword();

        UserControl GetPasswordControl();
    }
}
