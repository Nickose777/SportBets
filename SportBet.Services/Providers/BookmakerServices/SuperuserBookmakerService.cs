using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.BookmakerServices
{
    class SuperuserBookmakerService : IBookmakerService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserBookmakerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Delete(BookmakerDisplayDTO bookmaker)
        {
            string message = "";
            bool success = true;

            string login = bookmaker.Login;

            try
            {
                var users = unitOfWork.Users.GetAll(user => user.Login == login);
                UserEntity userEntity = users.SingleOrDefault();

                if (userEntity != null)
                {
                    int id = userEntity.Id;
                    BookmakerEntity bookmakerEntity = unitOfWork.Bookmakers.Get(id);
                    bookmakerEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteBookmaker(login);

                    unitOfWork.Commit();

                    message = String.Format("Successfully deleted user '{0}'", login);
                }
                else
                {
                    message = String.Format("User with login '{0}' was not found", login);
                    success = false;
                }
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

            return new ServiceMessage(message, success);
        }

        public DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<BookmakerDisplayDTO> bookmakers = null;

            try
            {
                IEnumerable<BookmakerEntity> bookmakerEntities = unitOfWork.Bookmakers.GetAll(b => !b.IsDeleted);
                IEnumerable<UserEntity> users = unitOfWork.Users.GetAll();

                bookmakers = bookmakerEntities.Select(bookmakerEntity =>
                {
                    string login = users.Single(user => user.Id == bookmakerEntity.Id).Login;
                    return new BookmakerDisplayDTO
                    {
                        Login = login,
                        FirstName = bookmakerEntity.FirstName,
                        LastName = bookmakerEntity.LastName,
                        PhoneNumber = bookmakerEntity.PhoneNumber
                    };
                }).ToList();

                message = "Successfully got all bookmakers!";
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

            return new DataServiceMessage<IEnumerable<BookmakerDisplayDTO>>(bookmakers, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
