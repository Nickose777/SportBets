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
            string message = "";
            bool success = true;

            if (Validate(clientEditDTO, ref message))
            {
                try
                {
                    int id = unitOfWork.Users.GetIdByLogin(login);
                    ClientEntity clientEntity = unitOfWork.Clients.Get(id);

                    clientEntity.FirstName = clientEditDTO.FirstName;
                    clientEntity.LastName = clientEditDTO.LastName;
                    clientEntity.PhoneNumber = clientEditDTO.PhoneNumber;
                    clientEntity.DateOfBirth = clientEditDTO.DateOfBirth;

                    unitOfWork.Commit();

                    message = "Updated client's info";
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }
            else
            {
                success = false;
            }

            return new ServiceMessage(message, success);
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
            string message = "";
            bool success = true;
            ClientEditDTO clientEditDTO = null;

            try
            {
                int id = unitOfWork.Users.GetIdByLogin(login);
                ClientEntity clientEntity = unitOfWork.Clients.Get(id);

                clientEditDTO = new ClientEditDTO
                {
                    FirstName = clientEntity.FirstName,
                    LastName = clientEntity.LastName,
                    PhoneNumber = clientEntity.PhoneNumber,
                    DateOfBirth = clientEntity.DateOfBirth
                };

                message = "Retrieved info about client";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<ClientEditDTO>(clientEditDTO, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(ClientEditDTO clientEditDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(clientEditDTO.LastName))
            {
                message = "Last name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(clientEditDTO.FirstName))
            {
                message = "First name must not be empty";
                isValid = false;
            }
            else if (String.IsNullOrEmpty(clientEditDTO.PhoneNumber))
            {
                message = "Phone number name must not be empty";
                isValid = false;
            }
            else if (clientEditDTO.DateOfBirth.Year < DateTime.Now.Year - 100)
            {
                message = "You are too old. Sorry";
                isValid = false;
            }
            else if (clientEditDTO.DateOfBirth.Year > DateTime.Now.Year - 18)
            {
                message = "You are too young. Sorry";
                isValid = false;
            }

            return isValid;
        }
    }
}
