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
    public class CommentControllerTest
    {
        private const string _loggedIn = "loggedIn";
        private const string _notLoggedIn = "";

        private readonly Mock<InterfaceCommentRepository> mockRepo = new Mock<InterfaceCommentRepository>();
        private readonly Mock<ILogger<CommentController>> mockLog = new Mock<ILogger<CommentController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        /********** Add comment **********/
        [Fact]
        public async Task AddCommentLogInOk()
        {
            // Arrange
            mockRepo.Setup(c => c.AddComment(It.IsAny<Comment>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.AddComment(It.IsAny<Comment>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task AddcommentNotLoggedInn()
        {
            // Arrange
            mockRepo.Setup(o => o.AddComment(It.IsAny<Comment>())).ReturnsAsync(true);

            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.AddComment(It.IsAny<Comment>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task AddCommentLogInCouldNotAdd()
        {
            // Arrange
            mockRepo.Setup(c => c.AddComment(It.IsAny<Comment>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.AddComment(It.IsAny<Comment>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Could not save comment", result.Value);
        }

        [Fact]
        public async Task AddCommentLogInModelStateInvalid()
        {
            // Arrange
            var comment = new Comment { Id = 1, Text = "@", DownVote = 1, UpVote = 1, ObservationId = 1 };

            mockRepo.Setup(c => c.AddComment(comment)).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            commentController.ModelState.AddModelError("Latitude", "Error in input validation");

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.AddComment(comment) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error in input validation", result.Value);
        }

        /********** Fetch all comments **********/
        [Fact]
        public async Task FetchAllCommentsOk()
        {
            // Arrange
            var comment1 = new Comment { Id = 1, Text = "comment", DownVote = 1, UpVote = 1, ObservationId = 1 };
            var comment2 = new Comment { Id = 2, Text = "second comment", DownVote = 1, UpVote = 1, ObservationId = 1 };
            var comment3 = new Comment { Id = 3, Text = "third comment", DownVote = 1, UpVote = 1, ObservationId = 1 };

            var commentList = new List<Comment> { comment1, comment2, comment3 };

            mockRepo.Setup(c => c.FetchAllComments(It.IsAny<int>())).ReturnsAsync(commentList);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.FetchAllComments(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal(commentList, (List<Comment>)result.Value);
        }

        [Fact]
        public async Task FetchAllCommentsDbError()
        {
            // Arrange
            mockRepo.Setup(c => c.FetchAllComments(It.IsAny<int>())).ReturnsAsync(() => null);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.FetchAllComments(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal("Table in database is empty", result.Value);
        }

        /********** Upvote comment **********/
        [Fact]
        public async Task UpvoteCommentLogInOk()
        {
            // Arrange
            mockRepo.Setup(c => c.UpVote(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.UpVote(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task UpvoteCommentNotLoggedInn()
        {
            // Arrange
            mockRepo.Setup(o => o.UpVote(It.IsAny<int>())).ReturnsAsync(false);

            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.UpVote(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task UpvoteCommentLogInCouldNotUpvote()
        {
            // Arrange
            mockRepo.Setup(c => c.UpVote(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.UpVote(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Could not up vote", result.Value);
        }

        /********** Downvote comment **********/
        [Fact]
        public async Task DownvoteCommentLogInOk()
        {
            // Arrange
            mockRepo.Setup(c => c.DownVote(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DownVote(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task DownvoteCommentNotLoggedInn()
        {
            // Arrange
            mockRepo.Setup(o => o.DownVote(It.IsAny<int>())).ReturnsAsync(false);

            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DownVote(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task DownvoteCommentLogInCouldNotDownvote()
        {
            // Arrange
            mockRepo.Setup(c => c.DownVote(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DownVote(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Could not down vote", result.Value);
        }

        /********** Delete comment **********/
        [Fact]
        public async Task DeleteCommentLogInOk()
        {
            // Arrange
            mockRepo.Setup(c => c.DeleteComment(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DeleteComment(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task DeleteCommentNotLoggedIn()
        {
            // Arrange
            mockRepo.Setup(o => o.DeleteComment(It.IsAny<int>())).ReturnsAsync(false);

            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _notLoggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DeleteComment(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Not logged in", result.Value);
        }

        [Fact]
        public async Task DeleteCommentLogInCouldNotDelete()
        {
            // Arrange
            mockRepo.Setup(c => c.DeleteComment(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            mockSession[_loggedIn] = _loggedIn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            commentController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await commentController.DeleteComment(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Could not delete comment", result.Value);
        }
    }
}
