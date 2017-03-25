using SportBet.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels;
using SportBet.Data.Contracts;
using SportBet.Core;
using SportBet.Data;
using SportBet.Core.Entities;
using SportBet.Services.Factories;

namespace SportBet.Services.Providers
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthService()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public AuthResult Register(ClientRegisterDTO clientRegisterDTO)
        {
            string message = "";
            bool success = true;

            if (!ValidateClient(clientRegisterDTO, ref message))
                success = false;
            else if (clientRegisterDTO.Password != clientRegisterDTO.ConfirmPassword)
            {
                success = false;
                message = "Passwords are not same";
            }
            else if (!ValidatePassword(clientRegisterDTO.Password, ref message))
                success = false;
            else
            {
                //TODO
                //Check for such login in DB
                //Hash password

                try
                {
                    unitOfWork.Accounts.RegisterClient(clientRegisterDTO.Login, clientRegisterDTO.Password);

                    UserEntity userEntity = new UserEntity
                    {
                        Login = clientRegisterDTO.Login,
                        Role = unitOfWork.Roles.Get(5)
                    };
                    unitOfWork.Users.Add(userEntity);
                    unitOfWork.Commit();

                    ClientEntity clientEntity = new ClientEntity
                    {
                        Id = userEntity.Id,
                        FirstName = clientRegisterDTO.FirstName,
                        LastName = clientRegisterDTO.LastName,
                        PhoneNumber = clientRegisterDTO.PhoneNumber,
                        DateOfBirth = clientRegisterDTO.DateOfBirth,
                        DateOfRegistration = DateTime.Now
                    };
                    unitOfWork.Clients.Add(clientEntity);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    success = false;
                }
                finally
                {

                }
            }

            return new AuthResult(message, success);
        }
        private bool ValidateClient(ClientRegisterDTO clientRegisterDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(clientRegisterDTO.LastName))
            {
                message = "Last name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(clientRegisterDTO.FirstName))
            {
                message = "First name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(clientRegisterDTO.PhoneNumber))
            {
                message = "Phone number name must not be empty";
                isValid = false;
            }
            else if (clientRegisterDTO.DateOfBirth.Year < DateTime.Now.Year - 100)
            {
                message = "You are too old. Sorry";
                isValid = false;
            }
            else if (clientRegisterDTO.DateOfBirth.Year > DateTime.Now.Year - 18)
            {
                message = "You are too young. Sorry";
                isValid = false;
            }

            return isValid;
        }
        private bool ValidatePassword(string password, ref string message)
        {
            const int MIN_LENGTH = 6;
            const int MAX_LENGTH = 15;

            bool isValid = true;

            if (String.IsNullOrEmpty(password))
            {
                message = "Enter the password";
                isValid = false;
            }
            else if (password.Length < MIN_LENGTH || password.Length > MAX_LENGTH)
            {
                message = "Password must consist of 6-15 characters";
                isValid = false;
            }
            else
            {
                bool hasUpperCaseLetter = false;
                bool hasLowerCaseLetter = false;
                bool hasDigit = false;

                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDigit = true;
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

        public IServiceFactory Login(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
