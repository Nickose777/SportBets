using SportBet.EventHandlers;

namespace SportBet.Contracts.Controllers
{
    //TODO rename
    public interface IController
    {
        event ServiceMessageEventHandler ReceivedMessage;
    }
}
