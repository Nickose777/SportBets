using SportBet.EventHandlers;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    public abstract class ControllerBase
    {
        public event ServiceMessageEventHandler ReceivedMessage;

        protected readonly ServiceFactory factory;

        public ControllerBase(ServiceFactory factory)
        {
            this.factory = factory;
        }

        protected void RaiseReceivedMessageEvent(bool success, string message)
        {
            var handler = ReceivedMessage;
            if (handler != null)
            {
                ServiceMessageEventArgs e = new ServiceMessageEventArgs(success, message);
                handler(this, e);
            }
        }
        protected void RaiseReceivedMessageEvent(object sender, ServiceMessageEventArgs e)
        {
            var handler = ReceivedMessage;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
