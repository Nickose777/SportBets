using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.Contracts;

namespace SportBet.Services.Providers.AdminServices
{
    class SuperuserAdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserAdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Update(AdminEditDTO adminEditDTO)
        {
            string message = "";
            bool success = true;

            if (success = Validate(adminEditDTO, ref message))
            {
                string login = adminEditDTO.Login;
                string firstName = adminEditDTO.FirstName;
                string lastName = adminEditDTO.LastName;
                string phoneNumber = adminEditDTO.PhoneNumber;

                try
                {
                    int id = unitOfWork.Users.GetIdByLogin(login);
                    AdminEntity adminEntity = unitOfWork.Admins.Get(id);

                    adminEntity.FirstName = firstName;
                    adminEntity.LastName = lastName;
                    adminEntity.PhoneNumber = phoneNumber;

                    unitOfWork.Commit();

                    message = "Changed admin's info";
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
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

                    message = String.Format("Deleted user '{0}'", login);
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
                })
                .OrderBy(admin => admin.LastName);

                message = "Got all admins";
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

        private bool Validate(AdminEditDTO adminEditDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(adminEditDTO.FirstName))
            {
                message = "First name cannot be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(adminEditDTO.LastName))
            {
                message = "Last name cannot be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(adminEditDTO.PhoneNumber))
            {
                message = "Phone number cannot be empty";
                isValid = false;
            }
            else
            {
                bool invalidPhone = adminEditDTO.PhoneNumber.Any(c => !Char.IsDigit(c));
                if (invalidPhone)
                {
                    message = "Phone number must consist only of digits";
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
