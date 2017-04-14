﻿using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;

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

        public ServiceMessage Register(ClientRegisterDTO clientRegisterDTO)
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
                    string adminLogin = "admin";
                    string adminPassword = unitOfWork.AdminPassword.GetPassword();

                    unitOfWork.Reconnect(adminLogin, adminPassword);

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

                    message = "Successfully registered new client!";
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
                finally
                {
                    unitOfWork.Dispose();
                }
            }

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            return new ServiceMessage("No permissions to register bookmaker", false);
        }

        public ServiceMessage Register(AdminRegisterDTO adminRegisterDTO)
        {
            return new ServiceMessage("No permissions to register admin", false);
        }

        public ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO)
        {
            return new ServiceMessage("No permissions to register analytic", false);
        }

        public ServiceMessage ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
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
