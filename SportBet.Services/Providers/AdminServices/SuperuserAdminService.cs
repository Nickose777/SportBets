﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.AdminServices
{
    class SuperuserAdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserAdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Delete(string login)
        {
            string message = "";
            bool success = true;

            try
            {
                UserEntity userEntity = unitOfWork.Users.GetUserByLogin(login);

                if (userEntity != null)
                {
                    int id = userEntity.Id;
                    AdminEntity adminEntity = unitOfWork.Admins.Get(id);
                    adminEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteAdminRole(login);

                    unitOfWork.Commit();

                    message = String.Format("Successfully deleted user '{0}'", login);
                }
                else
                {
                    message = String.Format("User with login '{0}' was not found", login);
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public DataServiceMessage<IEnumerable<AdminDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<AdminDisplayDTO> admins = null;

            try
            {
                IEnumerable<AdminEntity> adminEntities = unitOfWork.Admins.GetNotDeleted();
                IEnumerable<UserEntity> users = unitOfWork.Users.GetAll();

                admins = adminEntities.Select(adminEntity =>
                {
                    string login = users.Single(user => user.Id == adminEntity.Id).Login;
                    return new AdminDisplayDTO
                    {
                        Login = login,
                        FirstName = adminEntity.FirstName,
                        LastName = adminEntity.LastName,
                        PhoneNumber = adminEntity.PhoneNumber
                    };
                }).ToList();

                message = "Successfully got all admins!";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<AdminDisplayDTO>>(admins, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}