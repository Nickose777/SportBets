namespace SportBet.Models
{
    public class ChangePasswordModel
    {
        public string Login { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
