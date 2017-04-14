using System.Collections.Generic;
using SportBet.Contracts;
using SportBet.EventHandlers;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    public abstract class SubjectBase
    {
        public event ServiceMessageEventHandler ReceivedMessage;

        protected readonly ServiceFactory factory;
        protected List<IObserver> observers;

        public SubjectBase(ServiceFactory factory)
        {
            this.factory = factory;
            observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
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
