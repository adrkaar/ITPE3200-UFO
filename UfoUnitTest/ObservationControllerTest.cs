using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
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

        [Fact]
        public async Task SaveObservationOk()
        {
            // Arrange
            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.Equal("Observation was saved", result.Value);
        }

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

        [Fact]
        public async Task SaveObservationModelStateInvalid()
        {
            // Arrange
            var observation = new Observation
            {
                Id = 1,
                Date = "2022-10-10",
                Time = "12:12",
                Latitude = "latitude",
                Longitude = "123.45",
                Description = "I saw an ufo",
                UfoType = "Flat"
            };

            mockRepo.Setup(o => o.SaveObservation(observation)).ReturnsAsync(true);
            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            observationController.ModelState.AddModelError("Latitude", "Error in input validation");

            // Act
            var result = await observationController.SaveObservation(observation) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Error in input validation", result.Value);
        }
    }
}
