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
    public class CommentControllerTest
    {
        private readonly Mock<InterfaceCommentRepository> mockRepo = new Mock<InterfaceCommentRepository>();
        private readonly Mock<ILogger<CommentController>> mockLog = new Mock<ILogger<CommentController>>();

        /********** Add comment **********/
        // AddCommentLogInnOk
        [Fact]
        public async Task AddCommentOk()
        {
            // Arrange
            mockRepo.Setup(c => c.AddComment(It.IsAny<Comment>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.AddComment(It.IsAny<Comment>()) as ObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        //[Fact]
        //public async Task AddcommentNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // AddCommentLogInCouldNotAdd
        [Fact]
        public async Task AddCommentCouldNotAdd()
        {
            // Arrange
            mockRepo.Setup(c => c.AddComment(It.IsAny<Comment>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.AddComment(It.IsAny<Comment>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Could not save comment", result.Value);
        }

        // AddcommentLogInModelStateInvalid
        [Fact]
        public async Task AddCommentModelStateInvalid()
        {
            // Arrange
            var comment = new Comment { Id = 1, Text = "@", DownVote = 1, UpVote = 1, ObservationId = 1 };

            mockRepo.Setup(c => c.AddComment(comment)).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            commentController.ModelState.AddModelError("Latitude", "Error in input validation");

            // Act
            var result = await commentController.AddComment(comment) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Error in input validation", result.Value);
        }

        /********** Fetch all comments **********/
        // FetchAllCommentsLogInOk
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

        //[Fact]
        //public async Task FetchAllCommentsNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // FetchAllCommentsLogInDbError
        [Fact]
        public async Task FetchAllCommentsDbError()
        {
            // Arrange
            mockRepo.Setup(c => c.FetchAllComments(It.IsAny<int>())).ReturnsAsync(() => null);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.FetchAllComments(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Table in database is empty", result.Value);
        }

        /********** Upvote comment **********/
        // UpvoteCommentLogInOk
        [Fact]
        public async Task UpvoteCommentOk()
        {
            // Arrange
            mockRepo.Setup(c => c.UpVote(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.UpVote(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        //[Fact]
        //public async Task UpvoteCommentNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // UpvoteCommentCouldNotUpvote
        [Fact]
        public async Task UpvoteCommentCouldNotUpvote()
        {
            // Arrange
            mockRepo.Setup(c => c.UpVote(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.UpVote(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Could not up vote", result.Value);
        }

        /********** Downvote comment **********/
        // DownvoteCommentLogInOk
        [Fact]
        public async Task DownvoteCommentOk()
        {
            // Arrange
            mockRepo.Setup(c => c.DownVote(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.DownVote(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        //[Fact]
        //public async Task DownvoteCommentNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // UpvoteCommentCouldNotUpvote
        [Fact]
        public async Task DownvoteCommentCouldNotDownvote()
        {
            // Arrange
            mockRepo.Setup(c => c.DownVote(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.DownVote(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Could not down vote", result.Value);
        }

        /********** Delete comment **********/
        // DeleteCommentLogInOk
        [Fact]
        public async Task DeleteOk()
        {
            // Arrange
            mockRepo.Setup(c => c.DeleteComment(It.IsAny<int>())).ReturnsAsync(true);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.DeleteComment(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.True((bool)result.Value);
        }

        //[Fact]
        //public async Task DeleteCommentNotLoggedInn()
        //{
        // Arrange

        // Act

        // Assert
        //}

        // DeleteCommentCouldNotUpvote
        [Fact]
        public async Task DeleteCommentCouldNotDelete()
        {
            // Arrange
            mockRepo.Setup(c => c.DeleteComment(It.IsAny<int>())).ReturnsAsync(false);
            var commentController = new CommentController(mockRepo.Object, mockLog.Object);

            // Act
            var result = await commentController.DeleteComment(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal("Could not delete comment", result.Value);
        }
    }
}
