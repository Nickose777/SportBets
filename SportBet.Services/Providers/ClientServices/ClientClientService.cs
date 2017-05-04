using System;
using System.Collections.Generic;
using System.Text;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.Services.Contracts;

namespace SportBet.Services.Providers.ClientServices
{
    class ClientClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISession session;

        public ClientClientService(IUnitOfWork unitOfWork, ISession session)
        {
            this.unitOfWork = unitOfWork;
            this.session = session;
        }

        public ServiceMessage Update(ClientEditDTO clientEditDTO)
        {
            string message = null;
            bool success = true;

            if (clientEditDTO.Login == session.CurrentUserLogin)
            {
                if (Validate(clientEditDTO, ref message))
                {
                    try
                    {
                        int id = unitOfWork.Users.GetIdByLogin(clientEditDTO.Login);
                        ClientEntity clientEntity = unitOfWork.Clients.Get(id);

                        clientEntity.FirstName = clientEditDTO.FirstName;
                        clientEntity.LastName = clientEditDTO.LastName;
                        clientEntity.PhoneNumber = clientEditDTO.PhoneNumber;
                        clientEntity.DateOfBirth = clientEditDTO.DateOfBirth;

                        unitOfWork.Commit();

                        message = "Changed client's information";
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
            }
            else
            {
                message = "Session error: invalid session login. Access denied";
                success = false;
            }

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Delete(string login)
        {
            return new ServiceMessage("No permissions to delete a client", false);
        }

        public DataServiceMessage<ClientEditDTO> GetAuthorizedClientInfo()
        {
            string login = session.CurrentUserLogin;

            return GetClientInfo(login);
        }

        public DataServiceMessage<ClientEditDTO> GetClientInfo(string login)
        {
            ClientEditDTO clientDTO = null;
            string message = null;
            bool success = true;

            if (login == session.CurrentUserLogin)
            {
                try
                {
                    int id = unitOfWork.Users.GetIdByLogin(login);
                    ClientEntity clientEntity = unitOfWork.Clients.Get(id);

                    clientDTO = new ClientEditDTO
                    {
                        Login = login,
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
            }
            else
            {
                message = "Session error: invalid session login. Access denied";
                success = false;
            }

            return new DataServiceMessage<ClientEditDTO>(clientDTO, message, success);
        }

        public DataServiceMessage<IEnumerable<ClientDisplayDTO>> GetAll()
        {
            return new DataServiceMessage<IEnumerable<ClientDisplayDTO>>(null, "No permissions to get all clients", false);
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
