using SportBet.Contracts.Observers;

namespace SportBet.Contracts.Subjects
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void Notify();
    }
}
