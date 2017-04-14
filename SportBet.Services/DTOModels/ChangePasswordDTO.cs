namespace SportBet.Services.DTOModels
{
    //TODO
    //Confirm Password Validate logic also here
    public class ChangePasswordDTO
    {
        public string Login { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
