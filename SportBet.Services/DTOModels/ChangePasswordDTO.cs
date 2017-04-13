namespace SportBet.Services.DTOModels
{
    public class ChangePasswordDTO
    {
        public string Login { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
