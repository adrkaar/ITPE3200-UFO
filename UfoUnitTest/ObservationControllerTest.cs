using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Controllers;
using Ufo.DAL;
using Ufo.Models;
using Xunit;

namespace UfoUnitTest
{
    public class ObservationControllerTest
    {
        private readonly Mock<InterfaceObservationRepository> mockRepo = new Mock<InterfaceObservationRepository>();
        private readonly Mock<ILogger<ObservationController>> mockLog = new Mock<ILogger<ObservationController>>();

        /* Save observation */
        // gjøres om til SaveObservationsLogInnOk
        [Fact]
        public async Task SaveObservationOk()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        // må lage SaveObservationNotLoggedInn
        //[Fact]
        //public async Task SaveObservationNotLoggedInn()
        //{
        //    // Arrange
        //    //mockRep.Setup(k => k.Lagre(It.IsAny<Kunde>())).ReturnsAsync(true);

        //    //var kundeController = new KundeController(mockRep.Object, mockLog.Object);

        //    //mockSession[_loggetInn] = _ikkeLoggetInn;
        //    //mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    //kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    //// Act
        //    //var resultat = await kundeController.Lagre(It.IsAny<Kunde>()) as UnauthorizedObjectResult;

        //    //// Assert 
        //    //Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    //Assert.Equal("Ikke logget inn", resultat.Value);
        //    throw new NotImplementedException();
        //}

        // skal bli SaveObservationLogInnOkCanNotSave
        [Fact]
        public async Task SaveObservationCanNotSave()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(false);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.Equal("Observation was not saved", result.Value);
        }

        // skal bli SaveObservationLogInnOkModelStateInvalid
        [Fact]
        public async Task SaveObservationModelStateInvalid()
        {
            // Arrange
            var observation = new Observation { Id = 1, Date = "2022-10-10", Time = "12:12", Latitude = "latitude", Longitude = "123.45", Description = "I saw an ufo", UfoType = "Flat" };

            mockRepo.Setup(o => o.SaveObservation(observation)).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            observationController.ModelState.AddModelError("Latitude", "Error in input validation");

            // Act
            var result = await observationController.SaveObservation(observation) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Error in input validation", result.Value);
        }

        /* Fetch all observations */
        // FetchAllObservationsLogInnOk
        [Fact]
        public async Task FetchAllObservationsOk()
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
            Assert.Equal((List<Observation>)result.Value, observationList);
        }

        // FetchAllObservationsLogInnOkDbError
        [Fact]
        public async Task FetchAllObservationsDbError()
        {
            // Arrange
            var observationList = new List<Observation>();

            mockRepo.Setup(o => o.FetchAllObservations()).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchAllObservations() as OkObjectResult;

            //Assert
            Assert.Null(result);
        }

        // FetchAllObservationsNotLoggedInn
        //[Fact]
        //public async Task FetchAllObservationsNotLoggedInn()
        //{
        //    // Arrange
        //    //mockRep.Setup(k => k.HentAlle()).ReturnsAsync(It.IsAny<List<Kunde>>());

        //    //var kundeController = new KundeController(mockRep.Object, mockLog.Object);

        //    //mockSession[_loggetInn] = _ikkeLoggetInn;
        //    //mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        //    //kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

        //    //// Act
        //    //var resultat = await kundeController.HentAlle() as UnauthorizedObjectResult;

        //    //// Assert 
        //    //Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
        //    //Assert.Equal("Ikke logget inn", resultat.Value);
        //    throw new NotImplementedException();
        //}

        /* Fetch all ufotypes */
        // FetchAllUfotypesLogInnOk
        [Fact]
        public async Task FetchUfotypesOk()
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
            Assert.Equal((List<UfoType>)result.Value, ufoList);
        }

        // FetchAllUfotypesLogInnOkDbError
        [Fact]
        public async Task FetchUfotypesDbError()
        {
            // Arrange
            var ufoList = new List<UfoType>();

            mockRepo.Setup(o => o.FetchUfoTypes()).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchUfoTypes() as OkObjectResult;

            //Assert
            Assert.Null(result);
        }

        // FetchAllUfotypesNotLoggedInn
        //[Fact]
        //public async Task FetchUfotypesNotLoggedInn()
        //{
        //    // Arrange

        //    // Act

        //    //Assert
        //    throw new NotImplementedException();
        //}

        /* Fetch all locations */
        // FetchAllLocationsLogInnOk
        [Fact]
        public async Task FetchAllLocationsOk()
        {
            // Arrange
            var observation1 = new Observation { Date = "2022-09-21", Time = "22:22", Latitude = "56.66015", Longitude = "14.077921", UfoType = "Flat", Description = "I went outside and saw a big egg in the sky" };
            var observation2 = new Observation { Date = "2022-10-01", Time = "00:09", Latitude = "48.6479", Longitude = "9.8650", UfoType = "Round", Description = "I saw an UFO!!" };
            var observation3 = new Observation { Date = "2022-10-13", Time = "23:09", Latitude = "69.955420", Longitude = "23.139056", UfoType = "Big", Description = "I saw Swirling rivers of greenish-blue light in the sky, obviously from an UFO" };


            var locationList = new List<Observation> { observation1, observation2, observation3 };

            mockRepo.Setup(o => o.FetchAllLocations()).ReturnsAsync(locationList);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchAllLocations() as OkObjectResult;

            //Assert
            Assert.Equal((List<Observation>)result.Value, locationList);
        }

        // FetchAllLocationsLogInnOkDbError
        [Fact]
        public async Task FetchAllLocationsDbError()
        {
            // Arrange
            var locationList = new List<Observation>();

            mockRepo.Setup(o => o.FetchAllLocations()).ReturnsAsync(() => null);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.FetchAllLocations() as OkObjectResult;

            //Assert
            Assert.Null(result);
        }

        // FetchAllLocationsNotLoggedInn
        //[Fact]
        //public async Task FetchAllLocationsNotLoggedInn()
        //{
        //    // Arrange

        //    // Act

        //    //Assert
        //    throw new NotImplementedException();
        //}

        /* Fetch one Observation */
        // FetchOneObservationLogInOk
        [Fact]
        public async Task FetchOneObservationOk()
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

        // FetchOneObservationNotLoggedInn
        //[Fact]
        //public async Task FetchOneObservationOk()
        //{
        //    // Arrange

        //    // Act

        //    // Assert
        //}

        // FetchOneObservationCouldNotFetch
        [Fact]
        public async Task FetchOneObservationCouldNotFetch()
        {
            // Arrange
            mockRepo.Setup(o => o.GetOneObservation(It.IsAny<int>())).ReturnsAsync(() => null);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.GetOneObservation(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Observation was not found", result.Value);
        }

        /* change observation */
        // ChangeObservationLogInnOk
        [Fact]
        public async Task ChangeObservationOk()
        {
            // Arrange
            mockRepo.Setup(o => o.ChangeObservation(It.IsAny<Observation>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.ChangeObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        // changeObservationNotLoggedInn

        // ChangeObservationLogInnCouldNotChange
        [Fact]
        public async Task ChangeObservationCouldNotChange()
        {
            // Arrange
            mockRepo.Setup(o => o.ChangeObservation(It.IsAny<Observation>())).ReturnsAsync(false);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.ChangeObservation(It.IsAny<Observation>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Observation could not be changed", result.Value);
        }

        // ChangeObservationLogInModelStateInvalid
        [Fact]
        public async Task ChangeObservationModelStateInvalid()
        {
            // Arrange
            var observation = new Observation { Id = 1, Date = "2022-10-10", Time = "12:12", Latitude = "latitude", Longitude = "123.45", Description = "I saw an ufo", UfoType = "Flat" };

            mockRepo.Setup(o => o.ChangeObservation(observation)).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            observationController.ModelState.AddModelError("Latitude", "Error in input validation");

            // Act
            var result = await observationController.ChangeObservation(observation) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Error in input validation", result.Value);
        }

        /* delete observation */
        // DeleteObservationLogInnOk
        [Fact]
        public async Task DeleteObservationOk()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteObservation(It.IsAny<int>())).ReturnsAsync(true);
            var observationcontroller = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationcontroller.DeleteObservation(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        //[Fact]
        //public async Task DeleteObservationNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // DeleteObservationLogInnCouldNotDelete
        [Fact]
        public async Task DeleteObservationCouldNotDelete()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteObservation(It.IsAny<int>())).ReturnsAsync(false);
            var observationcontroller = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationcontroller.DeleteObservation(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Observation could not be deleted", result.Value);
        }
    }
}
