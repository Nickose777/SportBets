using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportBet.Data.Contracts;
using SportBet.Data.Contracts.Repositories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers;
using SportBet.Core.Entities;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace SportBet.Services.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private IAuthService service;

        [TestInitialize]
        public void Initialize()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            service = new AuthService(unitOfWork.Object);
        }

        [TestMethod]
        public void EstablishConnectionWithoutAdminCreateDefaultSuperuser()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>()));

            service.EstablishConnection();

            unitOfWork.Verify(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>()), Times.Once());
        }
        [TestMethod]
        public void EstablishConnectionWithAdminNoCreateDefaultSuperuser()
        {
            List<UserEntity> listWithOneUser = new List<UserEntity>() { new UserEntity() };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(listWithOneUser);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>()));

            service.EstablishConnection();

            unitOfWork.Verify(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>()), Times.Never());
        }
        [TestMethod]
        public void EstablishConnectionWithoutAdminCreateDefaultSuperuserReturnsTrue()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>()));

            bool established = service.EstablishConnection();

            Assert.IsTrue(established);
        }
        [TestMethod]
        public void EstablishConnectionUnitOfWorkThrowsExceptionReturnsFalse()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>())).Throws(new Exception());

            bool established = service.EstablishConnection();

            Assert.IsFalse(established);
        }

        [TestMethod]
        public void DisposesUnitOfWork()
        {
            unitOfWork.Setup(u => u.Dispose()).Verifiable();

            service.Dispose();
            unitOfWork.Verify();
        }
    }
}
