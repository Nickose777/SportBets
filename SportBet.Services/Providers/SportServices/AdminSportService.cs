using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;

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
                int sportsWithSameNameCount = unitOfWork.Countries.GetAll(country => country.Name == sportName).Count();
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

        public DataServiceMessage<IEnumerable<string>> GetAll()
        {
            string message;
            bool success = true;
            IEnumerable<string> sportNames = null;

            try
            {
                IEnumerable<SportEntity> sportEntities = unitOfWork.Sports.GetAll();
                sportNames = sportEntities.Select(countryEntity => countryEntity.Type);

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
