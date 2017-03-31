using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Validators
{
    public interface IRegisterValidator
    {
        bool Validate(RegisterBaseDTO registerBaseDTO, ref string message);
    }
}
