using SportBet.Core.Entities;
using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using SportBet.Services.Encryption;
using SportBet.Services.ResultTypes;
using SportBet.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.AccountServices
{
    public class SuperuserAccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRegisterValidator registerValidator;

        public SuperuserAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator)
        {
            this.unitOfWork = unitOfWork;
            this.registerValidator = registerValidator;
        }

        public AuthResult Register(ClientRegisterDTO clientRegisterDTO)
        {
            string message = "";
            bool success = true;
            IEnumerable<string> logins = null;

            try
            {
                logins = unitOfWork.Users.GetAll().Select(user => user.Login);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
            }

            if (!Validate(clientRegisterDTO, ref message))
                success = false;
            else if (!registerValidator.Validate(clientRegisterDTO, ref message))
                success = false;
            else if (logins.Contains(clientRegisterDTO.Login))
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
            IEnumerable<string> logins = null;

            try
            {
                logins = unitOfWork.Users.GetAll().Select(user => user.Login);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
            }

            if (success)
            {
                if (!Validate(bookmakerRegisterDTO, ref message))
                    success = false;
                else if (!registerValidator.Validate(bookmakerRegisterDTO, ref message))
                    success = false;
                else if (logins.Contains(bookmakerRegisterDTO.Login))
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
                            Role = unitOfWork.Roles.Get(4) //TODO remove this magic numbers
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
    }
}
