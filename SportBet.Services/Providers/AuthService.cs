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
using SportBet.Services.Encryption;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Validators;

namespace SportBet.Services.Providers
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRegisterValidator registerValidator;

        public AuthService()
        {
            this.unitOfWork = new UnitOfWork();
            this.registerValidator = new RegisterValidator();

            try
            {
                string hashedPassword = Hasher.EncodePassword("admin");
                unitOfWork.Accounts.CreateDefaultSuperuserIfNotExists(hashedPassword);
            }
            catch
            {

            }
        }

        public AuthResult Register(ClientRegisterDTO clientRegisterDTO)
        {
            string message = "";
            bool success = true;

            if (!Validate(clientRegisterDTO, ref message))
                success = false;
            else if (!registerValidator.Validate(clientRegisterDTO, ref message))
                success = false;
            else if (unitOfWork.Users.GetAll(user => user.Login == clientRegisterDTO.Login).Count() > 0)
            {
                success = false;
                message = "Such login already exists. Try another one";
            }
            else
            {
                string hashedPassword = Hasher.EncodePassword(clientRegisterDTO.Password);

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
        private bool Validate(ClientRegisterDTO clientRegisterDTO, ref string message)
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

        public AuthResult Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            string message = "";
            bool success = true;

            if (!Validate(bookmakerRegisterDTO, ref message))
                success = false;
            else if (!registerValidator.Validate(bookmakerRegisterDTO, ref message))
                success = false;
            else if (unitOfWork.Users.GetAll(user => user.Login == bookmakerRegisterDTO.Login).Count() > 0)
            {
                success = false;
                message = "Such login already exists. Try another one";
            }
            else
            {
                string hashedPassword = Hasher.EncodePassword(bookmakerRegisterDTO.Password);

                try
                {
                    unitOfWork.Accounts.RegisterBookmaker(bookmakerRegisterDTO.Login, hashedPassword);

                    UserEntity userEntity = new UserEntity
                    {
                        Login = bookmakerRegisterDTO.Login,
                        Role = unitOfWork.Roles.Get(5)
                    };
                    unitOfWork.Users.Add(userEntity);
                    unitOfWork.Commit();

                    BookmakerEntity bookmakerEntity = new BookmakerEntity
                    {
                        Id = userEntity.Id,
                        FirstName = bookmakerRegisterDTO.FirstName,
                        LastName = bookmakerRegisterDTO.LastName,
                        PhoneNumber = bookmakerRegisterDTO.PhoneNumber
                    };
                    unitOfWork.Bookmakers.Add(bookmakerEntity);

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
        private bool Validate(BookmakerRegisterDTO bookmakerRegisterDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(bookmakerRegisterDTO.LastName))
            {
                message = "Last name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(bookmakerRegisterDTO.FirstName))
            {
                message = "First name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(bookmakerRegisterDTO.PhoneNumber))
            {
                message = "Phone number name must not be empty";
                isValid = false;
            }

            return isValid;
        }

        public AuthServiceFactoryResult Login(UserLoginDTO userLoginDTO)
        {
            string message = "Successful login";
            bool success = true;
            LoginType loginType = LoginType.NoLogin;
            ServiceFactory factory = null;

            string login = userLoginDTO.Login;
            string password = userLoginDTO.Password;

            string hashedPassword = Hasher.EncodePassword(password);

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
                            loginType = LoginType.Superuser;
                            break;
                        case "Admin":
                            break;
                        case "Analytic":
                            break;
                        case "Bookmaker":
                            factory = new BookmakerServiceFactory(connectionString);
                            loginType = LoginType.Bookmaker;
                            break;
                        case "Client":
                            factory = new ClientServiceFactory(connectionString);
                            loginType = LoginType.Client;
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

            return new AuthServiceFactoryResult(factory, loginType, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
