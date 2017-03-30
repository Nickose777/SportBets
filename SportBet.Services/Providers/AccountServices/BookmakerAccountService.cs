using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using SportBet.Services.Encryption;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.AccountServices
{
    class BookmakerAccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRegisterValidator registerValidator;
        private readonly IEncryptor encryptor;

        public BookmakerAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor)
        {
            this.unitOfWork = unitOfWork;
            this.registerValidator = registerValidator;
            this.encryptor = encryptor;
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
                string hashedPassword = encryptor.Encrypt(clientRegisterDTO.Password);

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
            return new AuthResult("No permissions to register bookmaker", false);
        }
    }
}
