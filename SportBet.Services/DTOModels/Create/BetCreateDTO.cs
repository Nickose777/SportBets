using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Create
{
    public class BetCreateDTO : BetBaseDTO
    {
        public string BookmakerPhoneNumber { get; set; }

        public decimal Sum { get; set; }
    }
}
