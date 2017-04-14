using System.Collections.Generic;
using SportBet.Contracts;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    public abstract class SubjectBase : ControllerBase
    {
        protected List<IObserver> observers;

        public SubjectBase(ServiceFactory factory)
            : base(factory)
        {
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
    }
}
