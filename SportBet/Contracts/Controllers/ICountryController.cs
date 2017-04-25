using System.Windows.Controls;
namespace SportBet.Contracts.Controllers
{
    public interface ICountryController : IReceiveMessage
    {
        void Add();

        void Display();

        UserControl GetDisplayControl();
    }
}
