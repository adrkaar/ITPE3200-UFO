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
        public async Task SaveObservationCanNotSave()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveObservationOk()
        {
            // Arrange
            //var observation = new Observation
            //{
            //    Id = 1,
            //    Date = "2022-10-10",
            //    Time = "13:45",
            //    Latitude = "72.3",
            //    Longitude = "43.6",
            //    Description = "I saw an ufo",
            //    UfoType = "Flat"
            //};

            mockRepo.Setup(o => o.SaveObservation(It.IsAny<Observation>())).ReturnsAsync(true);

            var observationController = new ObservationController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await observationController.SaveObservation(It.IsAny<Observation>()) as ObjectResult;

            // Assert
            Assert.Equal("Observation was saved", result.Value);
        }
    }
}
