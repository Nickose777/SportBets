namespace SportBet.Services.DTOModels.Register
{
    public abstract class RegisterBaseDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
