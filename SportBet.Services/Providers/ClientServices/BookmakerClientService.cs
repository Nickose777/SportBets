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
    class BookmakerClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Update(ClientEditDTO clientEditDTO, string login)
        {
            throw new NotImplementedException();
        }

        public ServiceMessage Delete(string login)
        {
            return new ServiceMessage("No permissions to delete a client", false);
        }

        public DataServiceMessage<IEnumerable<ClientDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<ClientDisplayDTO> clients = null;

            try
            {
                IEnumerable<ClientEntity> clientEntities = unitOfWork.Clients.GetNotDeleted();
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
