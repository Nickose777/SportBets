using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Providers.AnalyticServices
{
    class SuperuserAnalyticService : IAnalyticService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserAnalyticService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Delete(AnalyticDisplayDTO analyticDisplayDTO)
        {
            string message = "";
            bool success = true;

            string login = analyticDisplayDTO.Login;

            try
            {
                var users = unitOfWork.Users.GetAll(user => user.Login == login);
                UserEntity userEntity = users.SingleOrDefault();

                if (userEntity != null)
                {
                    int id = userEntity.Id;
                    AnalyticEntity analyticEntity = unitOfWork.Analytics.Get(id);
                    analyticEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteAnalytic(login);

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

        public DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<AnalyticDisplayDTO> analytics = null;

            try
            {
                IEnumerable<AnalyticEntity> analyticEntities = unitOfWork.Analytics.GetAll(analytic => !analytic.IsDeleted);
                IEnumerable<UserEntity> users = unitOfWork.Users.GetAll();

                analytics = analyticEntities.Select(analyticEntity =>
                {
                    string login = users.Single(user => user.Id == analyticEntity.Id).Login;
                    return new AnalyticDisplayDTO
                    {
                        Login = login,
                        FirstName = analyticEntity.FirstName,
                        LastName = analyticEntity.LastName,
                        PhoneNumber = analyticEntity.PhoneNumber
                    };
                }).ToList();

                message = "Successfully got all analytics!";
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

            return new DataServiceMessage<IEnumerable<AnalyticDisplayDTO>>(analytics, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
