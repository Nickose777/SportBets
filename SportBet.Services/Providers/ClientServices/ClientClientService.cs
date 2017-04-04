﻿using SportBet.Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels;
using SportBet.Data.Contracts;
using SportBet.Core.Entities;

namespace SportBet.Services.Providers.ClientServices
{
    public class ClientClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Delete(ClientDisplayDTO clientDisplayDTO)
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
                    StringBuilder builder = new StringBuilder();
                    do
                    {
                        builder.Append(ex.Message + "; ");
                        ex = ex.InnerException;
                    } while (ex != null);

                    message = "Internal server errors: " + builder.ToString();
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
