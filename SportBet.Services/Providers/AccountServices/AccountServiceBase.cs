using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.AccountServices
{
    public abstract class AccountServiceBase : IAccountService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRegisterValidator registerValidator;
        protected readonly IEncryptor encryptor;
        protected readonly ISession session;

        public AccountServiceBase(IUnitOfWork unitOfWork, IRegisterValidator registerValidator, IEncryptor encryptor, ISession session)
        {
            this.unitOfWork = unitOfWork;
            this.registerValidator = registerValidator;
            this.encryptor = encryptor;
            this.session = session;
        }

        public virtual ServiceMessage Register(ClientRegisterDTO clientRegisterDTO)
        {
            return new ServiceMessage("No permissions to register clients", false);
        }

        public virtual ServiceMessage Register(BookmakerRegisterDTO bookmakerRegisterDTO)
        {
            return new ServiceMessage("No permissions to register bookmaker", false);
        }

        public virtual ServiceMessage Register(AdminRegisterDTO adminRegisterDTO)
        {
            return new ServiceMessage("No permissions to register admin", false);
        }

        public virtual ServiceMessage Register(AnalyticRegisterDTO analyticRegisterDTO)
        {
            return new ServiceMessage("No permissions to register analytic", false);
        }

        public ServiceMessage ChangePassword(DTOModels.ChangePasswordDTO changePasswordDTO)
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
                            OnPasswordChange(changePasswordDTO.Login, newHashedPassword);

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

        protected abstract void OnPasswordChange(string login, string newHashedPassword);

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
