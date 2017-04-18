using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.Factories;
using SportBet.Services.Providers;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IEncryptor> encryptor;
        private IAuthService service;

        [TestInitialize]
        public void Initialize()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            encryptor = new Mock<IEncryptor>();

            service = new AuthService(unitOfWork.Object, encryptor.Object);
        }

        [TestMethod]
        public void EstablishConnectionWithoutAdminCreateDefaultSuperuser()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()));

            service.EstablishConnection();

            unitOfWork.Verify(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void EstablishConnectionWithAdminNoCreateDefaultSuperuser()
        {
            List<UserEntity> listWithOneUser = new List<UserEntity>() { new UserEntity() };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(listWithOneUser);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()));

            service.EstablishConnection();

            unitOfWork.Verify(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void EstablishConnectionWithoutAdminSetAdminPassword()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork
                .Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork
                .Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()));
            unitOfWork
                .Setup(u => u.AdminPassword.SetPassword(It.IsAny<string>()))
                .Verifiable("Set password was not called");

            service.EstablishConnection();

            unitOfWork.VerifyAll();
        }

        [TestMethod]
        public void EstablishConnectionWithoutAdminCreateDefaultSuperuserReturnsTrue()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork
                .Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork
                .Setup(u => u.AdminPassword.SetPassword(It.IsAny<string>()));
            unitOfWork
                .Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>()));

            bool established = service.EstablishConnection();

            Assert.IsTrue(established);
        }

        [TestMethod]
        public void EstablishConnectionUnitOfWorkThrowsExceptionReturnsFalse()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            unitOfWork.Setup(u => u.Accounts.CreateDefaultSuperuser(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            bool established = service.EstablishConnection();

            Assert.IsFalse(established);
        }

        [TestMethod]
        public void DefaultPasswordToEncryptIsAdmin()
        {
            List<UserEntity> emptyList = new List<UserEntity>();
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns(emptyList);
            encryptor.Setup(e => e.Encrypt(It.Is<string>(password => password == "admin"))).Verifiable("Encrypt method was not called");

            service.EstablishConnection();

            encryptor.Verify();
        }

        [TestMethod]
        public void LoginPasswordIsEncrypted()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            encryptor.Setup(e => e.Encrypt(It.IsAny<string>())).Verifiable("Encrypt method was not called");

            service.Login(userLogin);

            encryptor.VerifyAll();
        }

        [TestMethod]
        public void LoginUnitOfWorkReconnectThrowsExceptionReturnsFalse()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            unitOfWork.Setup(u => u.Reconnect(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void LoginAsSuperuserReturnsResultForSuperuser()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            UserEntity userEntity = new UserEntity
            {
                Role = new RoleEntity { Name = "Superuser" }
            };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(new List<UserEntity>() { userEntity });

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual<LoginType>(result.LoginType, LoginType.Superuser);
            Assert.IsInstanceOfType(result.Factory, typeof(SuperuserServiceFactory));
        }

        [TestMethod]
        public void LoginAsAdminReturnsResultForAdmin()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            UserEntity userEntity = new UserEntity
            {
                Role = new RoleEntity { Name = "Admin" }
            };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(new List<UserEntity>() { userEntity });

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual<LoginType>(result.LoginType, LoginType.Admin);
            Assert.IsInstanceOfType(result.Factory, typeof(AdminServiceFactory));
        }

        [TestMethod]
        public void LoginAsAnalyticReturnsResultForAnalytic()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            UserEntity userEntity = new UserEntity
            {
                Role = new RoleEntity { Name = "Analytic" }
            };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(new List<UserEntity>() { userEntity });

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual<LoginType>(result.LoginType, LoginType.Analytic);
            Assert.IsInstanceOfType(result.Factory, typeof(AnalyticServiceFactory));
        }

        [TestMethod]
        public void LoginAsBookmakerReturnsResultForBookmaker()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            UserEntity userEntity = new UserEntity
            {
                Role = new RoleEntity { Name = "Bookmaker" }
            };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(new List<UserEntity>() { userEntity });

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual<LoginType>(result.LoginType, LoginType.Bookmaker);
            Assert.IsInstanceOfType(result.Factory, typeof(BookmakerServiceFactory));
        }

        [TestMethod]
        public void LoginAsClientReturnsResultForClient()
        {
            UserLoginDTO userLogin = new UserLoginDTO();
            UserEntity userEntity = new UserEntity
            {
                Role = new RoleEntity { Name = "Client" }
            };
            unitOfWork.Setup(u => u.Users.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>())).Returns(new List<UserEntity>() { userEntity });

            LoginServiceMessage result = service.Login(userLogin);

            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual<LoginType>(result.LoginType, LoginType.Client);
            Assert.IsInstanceOfType(result.Factory, typeof(ClientServiceFactory));
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
