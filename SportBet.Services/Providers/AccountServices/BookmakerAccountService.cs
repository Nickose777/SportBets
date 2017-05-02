using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;

namespace SportBet.Services.Providers.AccountServices
{
    public class BookmakerAccountService : AccountServiceBase, IAccountService
    {
        public BookmakerAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor, ISession session)
            : base(unitOfWork, registerValidator, encryptor, session) { }

        protected override void OnPasswordChange(string login, string newHashedPassword)
        {

        }

        public override ServiceMessage Register(ClientRegisterDTO clientRegisterDTO)
        {
            string message = "";
            bool success = true;

            if (!Validate(clientRegisterDTO, ref message))
                success = false;
            else if (!registerValidator.Validate(clientRegisterDTO, ref message))
                success = false;
            else
            {
                string hashedPassword = encryptor.Encrypt(clientRegisterDTO.Password);

                try
                {
                    string adminLogin = "admin";
                    string adminPassword = unitOfWork.AdminPassword.GetPassword();

                    unitOfWork.Reconnect(adminLogin, adminPassword);

                    IEnumerable<string> logins = unitOfWork.Users.GetAll().Select(user => user.Login);
                    if (!logins.Contains(clientRegisterDTO.Login))
                    {
                        bool phoneNumberExists = unitOfWork.Clients.GetAll().Any(c => c.PhoneNumber == clientRegisterDTO.PhoneNumber);
                        if (!phoneNumberExists)
                        {
                            unitOfWork.Accounts.RegisterClientRole(clientRegisterDTO.Login, hashedPassword);

                            UserEntity userEntity = new UserEntity
                            {
                                Login = clientRegisterDTO.Login,
                                Role = unitOfWork.Roles.Get(RolesCodes.ClientRole)
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

                            string currentUserLogin = Session.CurrentUserLogin;
                            string currentUserPassword = Session.CurrentUserHashedPassword;
                            unitOfWork.Reconnect(currentUserLogin, currentUserPassword);

                            message = "Registered new client";
                        }
                        else
                        {
                            success = false;
                            message = "Such phone number already exists. Try another one";
                        }
                    }
                    else
                    {
                        success = false;
                        message = "Such login already exists. Try another one";
                    }
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
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
    }
}
