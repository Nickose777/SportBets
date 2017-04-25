using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.BookmakerServices
{
    class BookmakerBookmakerService : IBookmakerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISession session;

        public BookmakerBookmakerService(IUnitOfWork unitOfWork, ISession session)
        {
            this.unitOfWork = unitOfWork;
            this.session = session;
        }

        public DataServiceMessage<string> GetLoggedInBookmakersPhoneNumber()
        {
            string message = "";
            bool success = true;
            string phoneNumber = null;

            string login = session.CurrentUserLogin;

            try
            {
                int id = unitOfWork.Users.GetIdByLogin(login);
                BookmakerEntity bookmakerEntity = unitOfWork.Bookmakers.Get(id);

                if (bookmakerEntity != null)
                {
                    phoneNumber = bookmakerEntity.PhoneNumber;
                    message = "Got phone number";
                }
                else
                {
                    message = "Bookmaker not found";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<string>(phoneNumber, message, success);
        }

        public ServiceMessage Update(DTOModels.Edit.BookmakerEditDTO bookmakerEditDTO, string login)
        {
            return new ServiceMessage("No permissions", false);
        }

        public ServiceMessage Delete(string login)
        {
            return new ServiceMessage("No permissions", false);
        }

        public DataServiceMessage<IEnumerable<DTOModels.Display.BookmakerDisplayDTO>> GetAll()
        {
            return new DataServiceMessage<IEnumerable<DTOModels.Display.BookmakerDisplayDTO>>(null, "No permissions", false);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
