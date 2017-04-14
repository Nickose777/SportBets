using System.Collections.Generic;
using SportBet.EventHandlers;
using SportBet.Services.Contracts;

namespace SportBet.Contracts
{
    public abstract class FacadeBase<T> where T : class
    {
        public event ServiceMessageEventHandler ReceivedMessage;

        protected readonly ServiceFactory factory;

        public FacadeBase(ServiceFactory factory)
        {
            this.factory = factory;
        }

        public abstract IEnumerable<T> GetAll();

        protected void RaiseReveivedMessageEvent(bool success, string message)
        {
            var handler = ReceivedMessage;
            if (handler != null)
            {
                ServiceMessageEventArgs e = new ServiceMessageEventArgs(success, message);
                handler(this, e);
            }
        }
    }
}
