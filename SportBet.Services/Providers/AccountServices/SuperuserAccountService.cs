using System;
using System.Collections.Generic;
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

namespace SportBet.Services.Providers.AccountServices
{
    public class SuperuserAccountService : AccountServiceBase, IAccountService
    {
        public SuperuserAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor, ISession session)
            : base(unitOfWork, registerValidator, encryptor, session) { }

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

        public override ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            string message = "";
            bool success = true;

            if (!Validate(bookmakerRegisterDTO, ref message))
                success = false;
            else if (!registerValidator.Validate(bookmakerRegisterDTO, ref message))
                success = false;
            else
            {
                string hashedPassword = encryptor.Encrypt(bookmakerRegisterDTO.Password);

                try
                {
                    IEnumerable<string> logins = unitOfWork.Users.GetAll().Select(user => user.Login);
                    if (!logins.Contains(bookmakerRegisterDTO.Login))
                    {
                        bool phoneNumberExists = unitOfWork.Bookmakers.GetAll().Any(b => b.PhoneNumber == bookmakerRegisterDTO.PhoneNumber);
                        if (!phoneNumberExists)
                        {
                            unitOfWork.Accounts.RegisterBookmakerRole(bookmakerRegisterDTO.Login, hashedPassword);

                            UserEntity userEntity = new UserEntity
                            {
                                Login = bookmakerRegisterDTO.Login,
                                Role = unitOfWork.Roles.Get(RolesCodes.BookmakerRole)
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
                            message = "Registered new bookmaker";
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

        public override ServiceMessage Register(AdminRegisterDTO adminRegisterDTO)
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
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            if (success)
            {
                if (!Validate(adminRegisterDTO, ref message))
                    success = false;
                else if (!registerValidator.Validate(adminRegisterDTO, ref message))
                    success = false;
                else if (logins.Contains(adminRegisterDTO.Login))
                {
                    success = false;
                    message = "Such login already exists. Try another one";
                }
                else
                {
                    string hashedPassword = encryptor.Encrypt(adminRegisterDTO.Password);

                    try
                    {
                        unitOfWork.Accounts.RegisterAdminRole(adminRegisterDTO.Login, hashedPassword);

                        UserEntity userEntity = new UserEntity
                        {
                            Login = adminRegisterDTO.Login,
                            Role = unitOfWork.Roles.Get(RolesCodes.AdminRole)
                        };
                        unitOfWork.Users.Add(userEntity);
                        unitOfWork.Commit();

                        AdminEntity adminEntity = new AdminEntity
                        {
                            Id = userEntity.Id,
                            FirstName = adminRegisterDTO.FirstName,
                            LastName = adminRegisterDTO.LastName,
                            PhoneNumber = adminRegisterDTO.PhoneNumber
                        };
                        unitOfWork.Admins.Add(adminEntity);
                        unitOfWork.Commit();

                        message = "Registered new admin";
                    }
                    catch (Exception ex)
                    {
                        message = ExceptionMessageBuilder.BuildMessage(ex);
                        success = false;
                    }
                }
            }

            return new ServiceMessage(message, success);
        }

        public override ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO)
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
                if (!Validate(analyticRegisterDTO, ref message))
                    success = false;
                else if (!registerValidator.Validate(analyticRegisterDTO, ref message))
                    success = false;
                else if (logins.Contains(analyticRegisterDTO.Login))
                {
                    success = false;
                    message = "Such login already exists. Try another one";
                }
                else
                {
                    string hashedPassword = encryptor.Encrypt(analyticRegisterDTO.Password);

                    try
                    {
                        unitOfWork.Accounts.RegisterAnalyticRole(analyticRegisterDTO.Login, hashedPassword);

                        UserEntity userEntity = new UserEntity
                        {
                            Login = analyticRegisterDTO.Login,
                            Role = unitOfWork.Roles.Get(RolesCodes.AnalyticRole)
                        };
                        unitOfWork.Users.Add(userEntity);
                        unitOfWork.Commit();

                        AnalyticEntity analyticEntity = new AnalyticEntity
                        {
                            Id = userEntity.Id,
                            FirstName = analyticRegisterDTO.FirstName,
                            LastName = analyticRegisterDTO.LastName,
                            PhoneNumber = analyticRegisterDTO.PhoneNumber
                        };
                        unitOfWork.Analytics.Add(analyticEntity);
                        unitOfWork.Commit();

                        message = "Registered new analytic";
                    }
                    catch (Exception ex)
                    {
                        message = ExceptionMessageBuilder.BuildMessage(ex);
                        success = false;
                    }
                }
            }

            return new ServiceMessage(message, success);
        }

        protected override void OnPasswordChange(string login, string newHashedPassword)
        {
            unitOfWork.Accounts.ChangePassword(login, newHashedPassword);
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

        private bool Validate(AdminRegisterDTO adminRegisterDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(adminRegisterDTO.LastName))
            {
                message = "Last name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(adminRegisterDTO.FirstName))
            {
                message = "First name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(adminRegisterDTO.PhoneNumber))
            {
                message = "Phone number name must not be empty";
                isValid = false;
            }

            return isValid;
        }

        private bool Validate(AnalyticRegisterDTO analyticRegisterDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(analyticRegisterDTO.LastName))
            {
                message = "Last name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(analyticRegisterDTO.FirstName))
            {
                message = "First name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(analyticRegisterDTO.PhoneNumber))
            {
                message = "Phone number name must not be empty";
                isValid = false;
            }

            return isValid;
        }
    }
}
