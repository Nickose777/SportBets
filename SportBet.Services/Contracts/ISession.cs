namespace SportBet.Services.Contracts
{
    public interface ISession
    {
        string CurrentUserLogin { get; set; }

        string CurrentUserHashedPassword { get; set; }
    }
}
