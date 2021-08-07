using Comments.Context;
using Comments.Entities;
using Comments.Entities.DTO;
using Comments.Entities.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Controller
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class CommentsController: ControllerBase
    {
        private readonly CommentsContext _context;
        private readonly IUserMock _user;

        public CommentsController(CommentsContext context, IUserMock user)
        {
            this._context = context;
            this._user = user;
        }

        /// <summary>
        /// Returns list of all comments
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.Comments.Include(ps => ps.Replies).ToList())
            );
        }

        /// <summary>
        /// Returns specific comment by its ID
        /// </summary>
        /// <param name="id">ID of comment</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var comment = _context.Comments.Find(id);
            _context.Entry(comment).Collection(comment => comment.Replies).Load();

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(comment));
        }

        /// <summary>
        /// Deletes specific comment by its ID
        /// </summary>
        /// <param name="id">ID of comment that will be deleted</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteById(int id, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var deletedComment = _context.Comments.Find(id);

            if (deletedComment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (_user.GetUserById(UserID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (deletedComment.CreatedByUserId!= UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.Comments.Remove(deletedComment);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedComment));
        }

        /// <summary>
        /// Creates new comment
        /// </summary>
        /// <param name="comment">Comment that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CommentDto comment, [FromHeader] int UserID)
        {
            if (_user.GetUserById(UserID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var newComment = new Comment()
            {
                CreatedByUserId = UserID,
                Text = comment.Text,
                PostId = comment.PostId
            };

            _context.Comments.Add(newComment);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newComment));
        }

        /// <summary>
        /// Updates specific comment by its ID
        /// </summary>
        /// <param name="id">ID of comment that will be updated</param>
        /// <param name="comment">Updated comment that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, [FromBody] CommentDto comment, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var currentComment = _context.Comments.Find(id);

            if (currentComment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (currentComment.CreatedByUserId!= UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            currentComment.Text = comment.Text;

            _context.Comments.Update(currentComment);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(currentComment));
        }

        /// <summary>
        /// Returns all replies for specific comment by its ID
        /// </summary>
        /// <param name="id">ID of comment whose replies will be listed</param>
        [HttpGet]
        [Route("{id}/Replies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRepliesForCommentById(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(comment).Collection(ps => ps.Replies).Load();

            return StatusCode(StatusCodes.Status200OK, new JsonResult(comment.Replies));
        }

        /// <summary>
        /// Creates new reply for comment by its ID
        /// </summary>
        /// <param name="id">ID of comment</param>
        /// <param name="commentReply">Reply that will be linked to specific comment</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("{id}/Replies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreateReplyForCommentById(int id, [FromBody] CommentReplyDto commentReply, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (_user.GetUserById(UserID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var newCommentReply = new CommentReply()
            {
                Text = commentReply.Text,
                UserId = UserID,
                Comment = comment
            };

            _context.CommentReplies.Add(newCommentReply);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newCommentReply));
        }

        /// <summary>
        /// Deletes specific comment reply from specific comment
        /// </summary>
        /// <param name="id">ID of comment</param>
        /// <param name="replyId">ID of reply which will be deleted</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}/Replies/{replyId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteReplyFromCommentById(int id, int replyId, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var comment = _context.Comments.Find(id);
            var reply = _context.CommentReplies.Find(replyId);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (_user.GetUserById(UserID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (reply.UserId!= UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.Entry(comment).Collection(comment => comment.Replies).Load();

            if (!comment.Replies.Any(cm => cm.Id== replyId))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var deletedCommentReply = _context.CommentReplies.Find(replyId);
            _context.CommentReplies.Remove(deletedCommentReply);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedCommentReply));
        }

        /// <summary>
        /// Updates specific comment reply by its ID
        /// </summary>
        /// <param name="replyId">ID of comment reply that will be updated</param>
        /// <param name="comment">Updated comment reply that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}/Replies/{replyId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult UpdateCommentReplyForCommentId(int replyId, [FromBody] CommentReplyDto commentReply, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var currentReply = _context.CommentReplies.Find(replyId);

            if (currentReply == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (_user.GetUserById(UserID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (currentReply.UserId != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            currentReply.Text = commentReply.Text;

            _context.CommentReplies.Update(currentReply);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(currentReply));
        }
    }
}
