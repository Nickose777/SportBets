namespace SportBet.Services
{
    static class Session
    {
        public static string CurrentUserLogin { get; set; }

        public static string CurrentUserHashedPassword { get; set; }
    }
}
