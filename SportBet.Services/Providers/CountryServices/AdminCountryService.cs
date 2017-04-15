using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.CountryServices
{
    class AdminCountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminCountryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(string countryName)
        {
            string message;
            bool success = true;

            try
            {
                int countriesWithSameNameCount = unitOfWork.Countries.GetAll(country => country.Name == countryName).Count();
                if (countriesWithSameNameCount == 0)
                {
                    unitOfWork.Countries.Add(new CountryEntity
                    {
                        Name = countryName
                    });
                    unitOfWork.Commit();

                    message = "Country added";
                }
                else
                {
                    message = "Country with such name already exists";
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

        public ServiceMessage Update(CountryEditDTO countryEditDTO)
        {
            string message;
            bool success = true;

            try
            {
                CountryEntity countryEntity = unitOfWork
                    .Countries
                    .GetAll()
                    .SingleOrDefault(country => country.Name == countryEditDTO.OldCountryName);
                if (countryEntity != null)
                {
                    countryEntity.Name = countryEditDTO.NewCountryName;
                    unitOfWork.Commit();

                    message = "Country was renamed";
                }
                else
                {
                    message = "Country with such name doesn't exist";
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
            IEnumerable<string> countryNames = null;

            try
            {
                IEnumerable<CountryEntity> countryEntities = unitOfWork
                    .Countries
                    .GetAll();
                countryNames = countryEntities
                    .Select(countryEntity => countryEntity.Name)
                    .OrderBy(name => name);

                message = "Got all countries";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<string>>(countryNames, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
