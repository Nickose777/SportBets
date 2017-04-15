using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.ClientServices
{
    class SuperuserClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Update(ClientEditDTO clientEditDTO, string login)
        {
            return new ServiceMessage("TODO - implement Update", false);
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
                    ClientEntity clientEntity = unitOfWork.Clients.Get(id);
                    clientEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteClientRole(login);

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

        public DataServiceMessage<IEnumerable<ClientDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<ClientDisplayDTO> clients = null;

            try
            {
                IEnumerable<ClientEntity> clientEntities = unitOfWork
                    .Clients
                    .GetNotDeleted();
                IEnumerable<UserEntity> users = unitOfWork
                    .Users
                    .GetAll();

                clients = clientEntities.Select(clientEntity =>
                {
                    string login = users
                        .Single(user => user.Id == clientEntity.Id)
                        .Login;

                    return new ClientDisplayDTO
                    {
                        Login = login,
                        FirstName = clientEntity.FirstName,
                        LastName = clientEntity.LastName,
                        PhoneNumber = clientEntity.PhoneNumber,
                        DateOfBirth = clientEntity.DateOfBirth,
                        DateOfRegistration = clientEntity.DateOfRegistration
                    };
                })
                .OrderBy(client => client.LastName);

                message = "Got all clients";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ClientDisplayDTO>>(clients, message, success);
        }

        public DataServiceMessage<ClientEditDTO> GetClientInfo(string login)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
