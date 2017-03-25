using SportBet.Services.Contracts.Services;
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
using SportBet.Services.Contracts.Factories;
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
            else if (unitOfWork.Users.GetAll(user => user.Login == clientRegisterDTO.Login).Count() > 0)
            {
                success = false;
                message = "Such login already exists. Try another one";
            }
            else
            {
                //TODO
                //Hash password
                string hashedPassword = clientRegisterDTO.Password;

                try
                {
                    unitOfWork.Accounts.RegisterClient(clientRegisterDTO.Login, hashedPassword);

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
                    unitOfWork.Dispose();
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
        //TODO PasswordValidator

        public AuthServiceFactoryResult Login(UserLoginDTO userLoginDTO)
        {
            string message = "Successful login";
            bool success = true;
            ServiceFactory factory = null;

            string login = userLoginDTO.Login;
            string password = userLoginDTO.Password;

            string hashedPassword = password; //TODO hash password

            string connectionString = String.Format("Server=127.0.0.1;Port=5432;Database=Bets;User Id={0};Password={1};", login, hashedPassword);

            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork(connectionString))
                {
                    UserEntity userEntity = unitOfWork.Users.GetAll(user => user.Login == login).FirstOrDefault();
                    string roleName = userEntity.Role.Name;

                    switch (roleName)
                    {
                        case "Superuser":
                            factory = new SuperUserServiceFactory(connectionString);
                            break;
                        case "Admin":
                            break;
                        case "Analytic":
                            break;
                        case "Bookmaker":
                            factory = new BookmakerServiceFactory(connectionString);
                            break;
                        case "Client":
                            factory = new ClientServiceFactory(connectionString);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            return new AuthServiceFactoryResult(factory, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
