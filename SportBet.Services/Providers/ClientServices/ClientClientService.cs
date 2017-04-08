﻿using System;
using System.Collections.Generic;
using System.Text;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.ClientServices
{
    class ClientClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Update(ClientEditDTO clientEditDTO, string login)
        {
            string message = null;
            bool success = true;

            if (login == Session.CurrentUserLogin)
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

                    message = "Successfully changed client's information";
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

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Delete(string login)
        {
            return new ServiceMessage("No permissions to delete a client", false);
        }

        public DataServiceMessage<ClientEditDTO> GetClientInfo(string login)
        {
            ClientEditDTO clientDTO = null;
            string message = null;
            bool success = true;

            if (login == Session.CurrentUserLogin)
            {
                try
                {
                    int id = unitOfWork.Users.GetIdByLogin(login);
                    ClientEntity clientEntity = unitOfWork.Clients.Get(id);

                    clientDTO = new ClientEditDTO
                    {
                        FirstName = clientEntity.FirstName,
                        LastName = clientEntity.LastName,
                        PhoneNumber = clientEntity.PhoneNumber,
                        DateOfBirth = clientEntity.DateOfBirth
                    };
                    message = "Successfully got information";
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
    }
}