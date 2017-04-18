using SportBet.EventHandlers;

namespace SportBet.Contracts.Controllers
{
    public interface IReceiveMessage
    {
        event ServiceMessageEventHandler ReceivedMessage;
    }
}
