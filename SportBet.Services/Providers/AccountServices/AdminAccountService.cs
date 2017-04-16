using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.Services.Contracts;

namespace SportBet.Services.Providers.AccountServices
{
    public class AdminAccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRegisterValidator registerValidator;
        private readonly IEncryptor encryptor;
        private readonly ISession session;

        public AdminAccountService(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor, ISession session)
        {
            this.unitOfWork = unitOfWork;
            this.registerValidator = registerValidator;
            this.encryptor = encryptor;
            this.session = session;
        }

        public ServiceMessage Register(ClientRegisterDTO clientRegisterDTO)
        {
            return new ServiceMessage("No permissions to register clients", false);
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
            string message = "";
            bool success = true;

            if (changePasswordDTO != null)
            {
                string oldPassword = changePasswordDTO.OldPassword;
                string oldHashedPassword = encryptor.Encrypt(oldPassword);
                string currentHashedPassword = session.CurrentUserHashedPassword;

                string newPassword = changePasswordDTO.NewPassword;

                if (oldHashedPassword == currentHashedPassword)
                {
                    if (success = registerValidator.ValidatePassword(newPassword, ref message))
                    {
                        string newHashedPassword = encryptor.Encrypt(newPassword);
                        try
                        {
                            unitOfWork.Accounts.ChangePassword(changePasswordDTO.Login, newHashedPassword);
                            unitOfWork.Commit();

                            message = "Password changed";
                        }
                        catch (Exception ex)
                        {
                            message = ExceptionMessageBuilder.BuildMessage(ex);
                            success = false;
                        }
                    }
                }
                else
                {
                    message = "Old password is incorrect";
                    success = false;
                }
            }
            else
            {
                message = "Bad argument. DTO cannot be null";
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
