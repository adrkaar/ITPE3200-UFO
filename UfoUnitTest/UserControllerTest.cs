using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Ufo.Controllers;
using Ufo.DAL;
using Ufo.Models;
using Xunit;

namespace UfoUnitTest
{
    public class UserControllerTest
    {
        private const string _loggedIn = "loggedIn";
        private const string _notLoggedIn = "";

        private readonly Mock<InterfaceUserRepository> mockRepo = new Mock<InterfaceUserRepository>();
        private readonly Mock<ILogger<UserController>> mockLog = new Mock<ILogger<UserController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task LogInOk()
        {
            // Assert
            mockRepo.Setup(k => k.LogIn(It.IsAny<User>())).ReturnsAsync(true);

            var userController = new UserController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.LogIn(It.IsAny<User>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LogInNotOk()
        {
            mockRepo.Setup(k => k.LogIn(It.IsAny<User>())).ReturnsAsync(false);

            var userController = new UserController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.LogIn(It.IsAny<User>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LogInModelStateInvalid()
        {
            mockRepo.Setup(k => k.LogIn(It.IsAny<User>())).ReturnsAsync(true);

            var userController = new UserController(mockRepo.Object, mockLog.Object);

            userController.ModelState.AddModelError("Username", "Error in input validation");

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await userController.LogIn(It.IsAny<User>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Error in input validation", resultat.Value);
        }

        [Fact]
        public void LogOut()
        {
            var userController = new UserController(mockRepo.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggedIn] = "";
            userController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            userController.LogOut();

            // Assert
            Assert.Equal(_notLoggedIn, mockSession[_loggedIn]);
        }
    }
}
