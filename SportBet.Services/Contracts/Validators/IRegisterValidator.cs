using SportBet.Services.DTOModels.Register;

namespace SportBet.Services.Contracts.Validators
{
    public interface IRegisterValidator
    {
        bool Validate(RegisterBaseDTO registerBaseDTO, ref string message);
    }
}
