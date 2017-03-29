﻿using SportBet.Services.Contracts.Services;
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

        public AuthService()
        {
            this.unitOfWork = new UnitOfWork();
        }
        public AuthService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool EstablishConnection()
        {
            bool established = true;

            try
            {
                string hashedPassword = Hasher.EncodePassword("admin");
                bool hasAdmin = unitOfWork.Users.GetAll(user => user.Login == "admin").Count() == 1;

                if (!hasAdmin)
                    unitOfWork.Accounts.CreateDefaultSuperuser(hashedPassword);
            }
            catch
            {
                established = false;
            }

            return established;
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
                        break;
                    case "Analytic":
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
