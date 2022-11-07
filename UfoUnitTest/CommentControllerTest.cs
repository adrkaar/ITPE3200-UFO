using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Ufo.Controllers;
using Ufo.DAL;
using Xunit;

namespace UfoUnitTest
{
    public class CommentControllerTest
    {
        private readonly Mock<InterfaceCommentRepository> mockRepo = new Mock<InterfaceCommentRepository>();
        private readonly Mock<ILogger<CommentController>> mockLog = new Mock<ILogger<CommentController>>();

        // AddCommentLogInnOk
        [Fact]
        public async Task AddCommentOk()
        {

        }

        //[Fact]
        //public async Task AddcommentNotLoggedInn()
        //{

        //}

        // AddCommentLogInCouldNotAdd
        [Fact]
        public async Task AddCommentCouldNotAdd()
        {

        }

        // AddcommentLogInModelStateInvalid
        [Fact]
        public async Task AddCommentModelStateInvalid()
        {

        }

        // FetchAllCommentsLogInOk
        [Fact]
        public async Task FetchAllCommentsOk()
        {

        }

        //[Fact]
        //public async Task FetchAllCommentsNotLoggedInn()
        //{

        //}

        // FetchAllCommentsLogInDbError
        [Fact]
        public async Task FetchAllCommentsDbError()
        {

        }

        // UpvoteCommentLogInOk
        [Fact]
        public async Task UpvoteCommentOk()
        {

        }

        //[Fact]
        //public async Task UpvoteCommentNotLoggedInn()
        //{

        //}

        // UpvoteCommentCouldNotUpvote
        [Fact]
        public async Task UpvoteCommentCouldNotUpvote()
        {

        }

        // DownvoteCommentLogInOk
        [Fact]
        public async Task DownvoteCommentOk()
        {

        }

        //[Fact]
        //public async Task DownvoteCommentNotLoggedInn()
        //{

        //}

        // UpvoteCommentCouldNotUpvote
        [Fact]
        public async Task DownvoteCommentCouldNotDownvote()
        {

        }

        // DeleteCommentLogInOk
        [Fact]
        public async Task DDeleteOk()
        {

        }

        //[Fact]
        //public async Task DeleteCommentNotLoggedInn()
        //{

        //}

        // DeleteCommentCouldNotUpvote
        [Fact]
        public async Task DeleteCommentCouldNotDelete()
        {

        }
    }
}
