using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.Encryption;
using SportBet.Services.Factories;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEncryptor encryptor;

        public AuthService()
        {
            this.unitOfWork = new UnitOfWork();
            this.encryptor = new Encryptor();
        }
        public AuthService(IUnitOfWork unitOfWork, IEncryptor encryptor)
        {
            this.unitOfWork = unitOfWork;
            this.encryptor = encryptor;
        }

        public bool EstablishConnection()
        {
            bool established = true;

            try
            {
                string hashedPassword = encryptor.Encrypt("admin");
                bool hasAdmin = unitOfWork.Users.GetAll(user => user.Login == "admin").Count() == 1;

                if (!hasAdmin)
                {
                    unitOfWork.Accounts.CreateDefaultSuperuser("admin", hashedPassword);
                    unitOfWork.Users.Add(new UserEntity { Login = "admin", RoleId = RolesCodes.SuperuserRole });

                    unitOfWork.AdminPassword.SetPassword(hashedPassword);

                    unitOfWork.Commit();
                }
            }
            catch
            {
                established = false;
            }

            return established;
        }

        public LoginServiceMessage Login(UserLoginDTO userLoginDTO)
        {
            string message = "Successful login";
            bool success = true;
            LoginType loginType = LoginType.NoLogin;
            ServiceFactory factory = null;

            string login = userLoginDTO.Login;
            string password = userLoginDTO.Password;

            string hashedPassword = encryptor.Encrypt(password);

            try
            {
                unitOfWork.Reconnect(login, hashedPassword);

                UserEntity userEntity = unitOfWork.Users.GetAll(user => user.Login == login).FirstOrDefault();
                string roleName = userEntity.Role.Name;

                switch (roleName)
                {
                    case "Superuser":
                        factory = new SuperuserServiceFactory(login, hashedPassword);
                        loginType = LoginType.Superuser;
                        break;
                    case "Admin":
                        factory = new AdminServiceFactory(login, hashedPassword);
                        loginType = LoginType.Admin;
                        break;
                    case "Analytic":
                        factory = new AnalyticServiceFactory(login, hashedPassword);
                        loginType = LoginType.Analytic;
                        break;
                    case "Bookmaker":
                        factory = new BookmakerServiceFactory(login, hashedPassword);
                        loginType = LoginType.Bookmaker;
                        break;
                    case "Client":
                        factory = new ClientServiceFactory(login, hashedPassword);
                        loginType = LoginType.Client;
                        break;
                    default:
                        break;
                }

                Session.CurrentUserLogin = login;
                Session.CurrentUserHashedPassword = hashedPassword;
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new LoginServiceMessage(factory, loginType, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
