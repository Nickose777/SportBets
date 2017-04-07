namespace SportBet.Models.Registers
{
    public abstract class RegisterBaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
