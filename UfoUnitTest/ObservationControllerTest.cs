using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ufo.Controllers;
using Ufo.DAL;
using Ufo.Models;
using Xunit;

namespace UfoUnitTest
{
    public class ObservationControllerTest
    {
        private const string _loggedIn = "loggedIn";
        private const string _notLoggedIn = "";

        private readonly Mock<InterfaceObservationRepository> mockRepo = new Mock<InterfaceObservationRepository>();
        private readonly Mock<ILogger<ObservationController>> mockLog = new Mock<ILogger<ObservationController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        /********** Save observation **********/
        [Fact]
        public async Task SaveObservationLogInOk()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task SaveObservationNotLoggedInn()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(false);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task SaveObservationLogInCanNotSave()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(false);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Observation was not saved", result.Value);
        }

        [Fact]
        public async Task SaveObservationLogInModelStateInvalid()
        {
            // Arrange
            var observation = new Observation { Id = 1, Date = "2022-10-10", Time = "12:12", Latitude = "latitude", Longitude = "123.45", Description = "I saw an ufo", UfoType = "Flat" };

            mockRepo.Setup(o => o.SaveObservation(observation)).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            observationController.ModelState.AddModelError("Latitude", "Error in input validation");

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.SaveObservation(observation) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error in input validation", result.Value);
        }

        /********** Fetch all observations **********/
        [Fact]
        public async Task FetchAllObservationsLogInOk()
        {
            // Arrange
            var observation1 = new Observation { Date = "2022-09-21", Time = "22:22", Latitude = "56.66015", Longitude = "14.077921", UfoType = "Flat", Description = "I went outside and saw a big egg in the sky" };
            var observation2 = new Observation { Date = "2022-10-01", Time = "00:09", Latitude = "48.6479", Longitude = "9.8650", UfoType = "Round", Description = "I saw an UFO!!" };
            var observation3 = new Observation { Date = "2022-10-13", Time = "23:09", Latitude = "69.955420", Longitude = "23.139056", UfoType = "Big", Description = "I saw Swirling rivers of greenish-blue light in the sky, obviously from an UFO" };

            var observationList = new List<Observation> { observation1, observation2, observation3 };

            mockRepo.Setup(o => o.FetchAllObservations()).ReturnsAsync(observationList);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchAllObservations() as OkObjectResult;

            //Assert
            Assert.Equal(observationList, (List<Observation>)result.Value);
        }

        [Fact]
        public async Task FetchAllObservationsLogInDbError()
        {
            // Arrange
            var observationList = new List<Observation>();

            mockRepo.Setup(o => o.FetchAllObservations()).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchAllObservations() as NotFoundObjectResult;

            //Assert
            Assert.Equal("Table in database is empty", result.Value);
        }

        /********** Fetch all ufotypes **********/
        [Fact]
        public async Task FetchUfotypesLogInOk()
        {
            // Arrange
            var type1 = new UfoType { Type = "Round" };
            var type2 = new UfoType { Type = "Flat" };
            var type3 = new UfoType { Type = "Big" };

            var ufoList = new List<UfoType> { type1, type2, type3 };

            mockRepo.Setup(o => o.FetchUfoTypes()).ReturnsAsync(ufoList);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchUfoTypes() as OkObjectResult;

            //Assert
            Assert.Equal(ufoList, (List<UfoType>)result.Value);
        }

        [Fact]
        public async Task FetchUfotypesLogInDbError()
        {
            // Arrange
            var ufoList = new List<UfoType>();

            mockRepo.Setup(o => o.FetchUfoTypes()).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchUfoTypes() as NotFoundObjectResult;

            //Assert
            Assert.Equal("Table in database is empty", result.Value);
        }

        /********** Fetch one Observation **********/
        [Fact]
        public async Task FetchOneObservationLogInOk()
        {
            // Arrange
            var observation = new Observation { Date = "2022-09-21", Time = "22:22", Latitude = "56.66015", Longitude = "14.077921", UfoType = "Flat", Description = "I went outside and saw a big egg in the sky" };

            mockRepo.Setup(o => o.GetOneObservation(It.IsAny<int>())).ReturnsAsync(observation);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.GetOneObservation(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal(observation, (Observation)result.Value);
        }

        [Fact]
        public async Task FetchOneObservationLogInCouldNotFetch()
        {
            // Arrange
            mockRepo.Setup(o => o.GetOneObservation(It.IsAny<int>())).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.GetOneObservation(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal("Observation was not found", result.Value);
        }

        /********** change observation **********/
        [Fact]
        public async Task ChangeObservationLogInOk()
        {
            // Arrange
            mockRepo.Setup(o => o.ChangeObservation(It.IsAny<Observation>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.ChangeObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task ChangeObservationNotLoggedIn()
        {
            // Arrange
            mockRepo.Setup(o => o.ChangeObservation(It.IsAny<Observation>())).ReturnsAsync(false);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.ChangeObservation(It.IsAny<Observation>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task ChangeObservationLogInCouldNotChange()
        {
            // Arrange
            mockRepo.Setup(o => o.ChangeObservation(It.IsAny<Observation>())).ReturnsAsync(false);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.ChangeObservation(It.IsAny<Observation>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Observation could not be changed", result.Value);
        }

        [Fact]
        public async Task ChangeObservationLogInModelStateInvalid()
        {
            // Arrange
            var observation = new Observation { Id = 1, Date = "2022-10-10", Time = "12:12", Latitude = "latitude", Longitude = "123.45", Description = "I saw an ufo", UfoType = "Flat" };

            mockRepo.Setup(o => o.ChangeObservation(observation)).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            observationController.ModelState.AddModelError("Latitude", "Error in input validation");

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.ChangeObservation(observation) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error in input validation", result.Value);
        }

        /********** delete observation **********/
        [Fact]
        public async Task DeleteObservationLogInOk()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteObservation(It.IsAny<int>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.DeleteObservation(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task DeleteObservationNotLoggedInn()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteObservation(It.IsAny<int>())).ReturnsAsync(false);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.DeleteObservation(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task DeleteObservationLogInCouldNotDelete()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteObservation(It.IsAny<int>())).ReturnsAsync(false);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            observationController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await observationController.DeleteObservation(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Observation could not be deleted", result.Value);
        }
    }
}
