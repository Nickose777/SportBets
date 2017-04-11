using SportBet.Contracts.Observers;

namespace SportBet.Contracts.Subjects
{
    public interface IClientSubject
    {
        void Subscribe(IClientObserver observer);

        void Unsubscribe(IClientObserver observer);

        void Notify();
    }
}
