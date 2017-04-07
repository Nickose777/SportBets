using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public ServiceMessage Delete(ClientDisplayDTO clientDisplayDTO)
        {
            string message = "";
            bool success = true;

            string login = clientDisplayDTO.Login;

            try
            {
                var users = unitOfWork.Users.GetAll(user => user.Login == login);
                UserEntity userEntity = users.SingleOrDefault();

                if (userEntity != null)
                {
                    //TODO
                    //some refactoring
                    //ClientDisplayDTO and BookmakerDisplayDTO to LoginDTO or just string login

                    int id = userEntity.Id;
                    ClientEntity clientEntity = unitOfWork.Clients.Get(id);
                    clientEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteClient(login);

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
                StringBuilder builder = new StringBuilder();
                do
                {
                    builder.AppendLine(ex.Message);
                    ex = ex.InnerException;
                } while (ex != null);

                message = "Internal server errors: " + builder.ToString();
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
                IEnumerable<ClientEntity> clientEntities = unitOfWork.Clients.GetAll(c => !c.IsDeleted);
                IEnumerable<UserEntity> users = unitOfWork.Users.GetAll();

                clients = clientEntities.Select(clientEntity =>
                {
                    string login = users.Single(user => user.Id == clientEntity.Id).Login;
                    return new ClientDisplayDTO
                    {
                        Login = login,
                        FirstName = clientEntity.FirstName,
                        LastName = clientEntity.LastName,
                        PhoneNumber = clientEntity.PhoneNumber,
                        DateOfBirth = clientEntity.DateOfBirth,
                        DateOfRegistration = clientEntity.DateOfRegistration
                    };
                });

                message = "Successfully got all clients!";
            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();
                do
                {
                    builder.AppendLine(ex.Message);
                    ex = ex.InnerException;
                } while (ex != null);

                message = "Internal server errors: " + builder.ToString();
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
