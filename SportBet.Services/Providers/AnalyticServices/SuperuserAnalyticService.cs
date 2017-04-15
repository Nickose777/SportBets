using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.AnalyticServices
{
    class SuperuserAnalyticService : IAnalyticService
    {
        private readonly IUnitOfWork unitOfWork;

        public SuperuserAnalyticService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Delete(string login)
        {
            string message = "";
            bool success = true;

            try
            {
                UserEntity userEntity = unitOfWork.Users.GetUserByLogin(login);

                if (userEntity != null)
                {
                    int id = userEntity.Id;
                    AnalyticEntity analyticEntity = unitOfWork.Analytics.Get(id);
                    analyticEntity.IsDeleted = true;

                    unitOfWork.Users.Remove(userEntity);
                    unitOfWork.Accounts.DeleteAnalyticRole(login);

                    unitOfWork.Commit();

                    message = String.Format("Deleted user '{0}'", login);
                }
                else
                {
                    message = String.Format("User with login '{0}' was not found", login);
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

        public DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<AnalyticDisplayDTO> analytics = null;

            try
            {
                IEnumerable<AnalyticEntity> analyticEntities = unitOfWork.Analytics.GetNotDeleted();
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
                })
                .OrderBy(analytic => analytic.LastName);

                message = "Got all analytics";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
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
