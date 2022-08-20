using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityService.Context;
using QualityService.Mocks;
using QualityService.Models;
using QualityService.Models.DTO;
using System.Linq;

namespace QualityService.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]

    public class FeedbackController : ControllerBase
    {
        private readonly QualityContext _context;
        private readonly IUserMock _user;
        private readonly IProductMock _product;

        public FeedbackController(QualityContext context)
        {
            this._context = context;
            this._user = new UserMock();
            this._product = new ProductMock();
        }

        // GET: Feedback
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllFeedbacks()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.Feedback.ToList())
            );
        }

        //GET BY ID: Feedback
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFeedback(int id)
        {
            var feedbackType = _context.Feedback.Find(id);
            if (feedbackType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(feedbackType));
        }
        //POST: Feedback
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreateReactionType([FromBody] FeedbackDTO feedback, [FromHeader] int UserId)
        {
            if (_user.GetUserById(UserId) == null) //&& UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var newFeedback = new Feedback()
            {

                UserId = UserId,
                ProductId = feedback.ProductId,
                IsPositive = feedback.IsPositive,
                IsSelling = feedback.IsSelling,
                Comment = feedback.Comment,
                Response = null
            };

            _context.Feedback.Add(newFeedback);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, new JsonResult(newFeedback));
        }

        /// <summary>
        //PUT: ReactionType
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackDTO feedback, [FromHeader] int UserId)
        {
            var currFeedback = _context.Feedback.Find(id);
            if (currFeedback == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserId) == null ) //&& UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            currFeedback.IsSelling = feedback.IsSelling;
            currFeedback.IsPositive = feedback.IsPositive;
            currFeedback.Comment = feedback.Comment;
            currFeedback.Response = feedback.Response;

            _context.Feedback.Update(currFeedback);
            var success = _context.SaveChanges();
            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
        //DELETE : Feedback
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteFeedback(int id, [FromHeader] int UserID)
        {
            var currFeedback = _context.Feedback.Find(id);
            if (currFeedback == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserID) == null) // && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            _context.Feedback.Remove(currFeedback);
            var success = _context.SaveChanges();
            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}
