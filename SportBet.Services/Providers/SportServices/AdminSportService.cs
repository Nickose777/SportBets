using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Providers.SportServices
{
    class AdminSportService : ISportService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminSportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(string sportName)
        {
            string message;
            bool success = true;

            try
            {
                int sportsWithSameNameCount = unitOfWork.Sports.GetAll(sport => sport.Type == sportName).Count();
                if (sportsWithSameNameCount == 0)
                {
                    unitOfWork.Sports.Add(new SportEntity
                    {
                        Type = sportName
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
                    .GetAll()
                    .SingleOrDefault(sport => sport.Type == sportEditDTO.OldSportName);
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
                    .Select(countryEntity => countryEntity.Type)
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
