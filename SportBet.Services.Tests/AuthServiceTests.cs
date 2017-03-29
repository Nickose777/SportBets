using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportBet.Data.Contracts;
using SportBet.Data.Contracts.Repositories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers;

namespace SportBet.Services.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void DisposesUnitOfWork()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Dispose()).Verifiable();
            IAuthService service = new AuthService(unitOfWork.Object);

            service.Dispose();
            unitOfWork.Verify();
        }
    }
}
