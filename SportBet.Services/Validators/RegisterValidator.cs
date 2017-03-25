using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Validators
{
    class RegisterValidator : IRegisterValidator
    {
        private const int MinLength = 6;
        private const int MaxLength = 15;

        public bool Validate(RegisterBaseDTO registerBaseDTO, ref string message)
        {
            bool isValid = true;

            string login = registerBaseDTO.Login;
            string password = registerBaseDTO.Password;
            string confirmPassword = registerBaseDTO.ConfirmPassword;

            if (String.IsNullOrEmpty(login))
            {
                message = "Login can't be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(password))
            {
                message = "Password can't be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(confirmPassword))
            {
                message = "You haven't confirmed your password";
                isValid = false;
            }
            else if (password != confirmPassword)
            {
                message = "Passwords are not equal";
                isValid = false;
            }
            else if (login.Length < 3)
            {
                message = "Login must contain at least 3 characters";
                isValid = false;
            }
            else if (!IsValidLogin(login))
            {
                message = "Login must contain only letters and digits";
                isValid = false;
            }
            else if (password.Length < MinLength || password.Length > MaxLength)
            {
                message = String.Format("Password must consist of {0}-{1} characters", MinLength, MaxLength);
                isValid = false;
            }
            else
            {
                bool hasUpperCaseLetter = false;
                bool hasLowerCaseLetter = false;
                bool hasDigit = false;

                foreach (char c in password)
                {
                    if (Char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (Char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (Char.IsDigit(c)) hasDigit = true;
                }

                isValid =
                    hasUpperCaseLetter &&
                    hasLowerCaseLetter &&
                    hasDigit;

                if (!isValid)
                    message = "Password must consist at least of 1 upper case, 1 lower case character and 1 digit";
            }

            return isValid;
        }

        private bool IsValidLogin(string login)
        {
            bool isValid = true;

            foreach (char ch in login)
            {
                if (!Char.IsLetterOrDigit(ch))
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
    }
}
