using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.ResultTypes;
using System;

namespace SportBet.Services.Tests.AccountServices
{
    [TestClass]
    public class AdminAccountServiceTests
    {
        private IAccountService service;

        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IRegisterValidator> registerValidator;
        private Mock<IEncryptor> encryptor;
        private Mock<ISession> session;

        [TestInitialize]
        public void Initialize()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            registerValidator = new Mock<IRegisterValidator>();
            encryptor = new Mock<IEncryptor>();
            session = new Mock<ISession>();

            service = new AdminAccountService(unitOfWork.Object, registerValidator.Object, encryptor.Object, session.Object);
        }

        [TestMethod]
        public void RegisterAdmin_NoPermissions_ReturnsFalse()
        {
            ServiceMessage serviceMessage = service.Register(It.IsAny<AdminRegisterDTO>());

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void RegisterClient_NoPermissions_ReturnsFalse()
        {
            ServiceMessage serviceMessage = service.Register(It.IsAny<ClientRegisterDTO>());

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void RegisterBookmaker_NoPermissions_ReturnsFalse()
        {
            ServiceMessage serviceMessage = service.Register(It.IsAny<BookmakerRegisterDTO>());

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void RegisterAnalytic_NoPermissions_ReturnsFalse()
        {
            ServiceMessage serviceMessage = service.Register(It.IsAny<AnalyticRegisterDTO>());

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void ChangePassword_InvalidChangePasswordDTO_ReturnsFalse()
        {
            ServiceMessage serviceMessage = service.ChangePassword(null);

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void ChangePassword_CallsEncryptMethod()
        {
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            encryptor
                .Setup(e => e.Encrypt(It.IsAny<string>()))
                .Verifiable("Encrypt method was not called");

            service.ChangePassword(changePasswordDTO);

            encryptor.VerifyAll();
        }

        [TestMethod]
        public void ChangePassword_CallsCurrentUserHashedPassword()
        {
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            session
                .SetupGet(s => s.CurrentUserHashedPassword)
                .Verifiable("CurrentUserHashedPassword getter was not called");

            service.ChangePassword(changePasswordDTO);

            session.VerifyAll();
        }

        [TestMethod]
        public void ChangePassword_PasswordIsNotConfirmed_ReturnsFalse()
        {
            string password1 = "1";
            string password2 = "2";
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            encryptor
                .Setup(e => e.Encrypt(It.IsAny<string>()))
                .Returns(password1);
            session
                .SetupGet(s => s.CurrentUserHashedPassword)
                .Returns(password2);

            ServiceMessage serviceMessage = service.ChangePassword(changePasswordDTO);

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void ChangePassword_CallsValidatePassword()
        {
            string message = "";
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            registerValidator
                .Setup(r => r.ValidatePassword(It.IsAny<string>(), ref message))
                .Verifiable("ValidatePassword method was not called");

            service.ChangePassword(changePasswordDTO);

            registerValidator.VerifyAll();
        }

        [TestMethod]
        public void ChangePassword_ValidationUnsuccessful_ReturnsFalse()
        {
            string message = "";
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            registerValidator
                .Setup(r => r.ValidatePassword(It.IsAny<string>(), ref message))
                .Returns(false);

            ServiceMessage serviceMessage = service.ChangePassword(changePasswordDTO);

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void ChangePassword_ValidationSuccessful_CallsChangePasswordMethod()
        {
            string message = "";
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            registerValidator
                .Setup(r => r.ValidatePassword(It.IsAny<string>(), ref message))
                .Returns(true);
            unitOfWork
                .Setup(u => u.Accounts.ChangePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable("ChangePassword method of Accounts was not called");

            service.ChangePassword(changePasswordDTO);

            unitOfWork.VerifyAll();
        }

        [TestMethod]
        public void ChangePassword_ThrowsException_ReturnsFalse()
        {
            string message = "";
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            registerValidator
                .Setup(r => r.ValidatePassword(It.IsAny<string>(), ref message))
                .Returns(true);
            unitOfWork
                .Setup(u => u.Commit())
                .Throws(new Exception());

            ServiceMessage serviceMessage = service.ChangePassword(changePasswordDTO);

            Assert.IsFalse(serviceMessage.IsSuccessful);
        }

        [TestMethod]
        public void Dispose_UnitOfWorkDisposed()
        {
            unitOfWork
                .Setup(u => u.Dispose())
                .Verifiable("Dispose method was not called");

            service.Dispose();

            unitOfWork.VerifyAll();
        }
    }
}
