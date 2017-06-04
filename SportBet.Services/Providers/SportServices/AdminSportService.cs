using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Create;

namespace SportBet.Services.Providers.SportServices
{
    class AdminSportService : ISportService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminSportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(SportCreateDTO sportCreateDTO)
        {
            string message;
            bool success = true;

            string sportName = sportCreateDTO.SportName;
            bool isDual = sportCreateDTO.IsDual;

            try
            {
                bool exists = unitOfWork.Sports.Exists(sportName);
                if (!exists)
                {
                    unitOfWork.Sports.Add(new SportEntity
                    {
                        Type = sportName,
                        IsDual = isDual
                    });
                    unitOfWork.Commit();

                    message = "Sport added";
                }
                else
                {
                    message = "Sport with such name already exists";
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

        public ServiceMessage Update(SportEditDTO sportEditDTO)
        {
            string message;
            bool success = true;

            try
            {
                SportEntity sportEntity = unitOfWork
                    .Sports
                    .Get(sportEditDTO.OldSportName);
                if (sportEntity != null)
                {
                    sportEntity.Type = sportEditDTO.NewSportName;
                    unitOfWork.Commit();

                    message = "Sport was renamed";
                }
                else
                {
                    message = "Sport with such name doesn't exist";
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

        public ServiceMessage Delete(string sportName)
        {
            string message;
            bool success = true;

            try
            {
                SportEntity sportEntity = unitOfWork.Sports.Get(sportName);
                if (sportEntity != null)
                {
                    unitOfWork.Sports.Remove(sportEntity);
                    unitOfWork.Commit();

                    message = "Sport deleted";
                }
                else
                {
                    message = "Sport with such name doesn't exist";
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

        public DataServiceMessage<IEnumerable<string>> GetAll()
        {
            string message;
            bool success = true;
            IEnumerable<string> sportNames = null;

            try
            {
                IEnumerable<SportEntity> sportEntities = unitOfWork
                    .Sports
                    .GetAll();
                sportNames = sportEntities
                    .Select(sportEntity => sportEntity.Type)
                    .OrderBy(name => name);

                message = "Got all sports";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<string>>(sportNames, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
