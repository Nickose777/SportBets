using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
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
                int countryCount = unitOfWork.Countries.GetAll(country => country.Name == countryName).Count();
                if (countryCount == 0)
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

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
