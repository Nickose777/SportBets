using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.SportServices
{
    class BookmakerSportService : ISportService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookmakerSportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(SportCreateDTO sportCreateDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Update(SportEditDTO sportEditDTO)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Delete(string sportName)
        {
            return new ServiceMessage("No permissions", false);
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
