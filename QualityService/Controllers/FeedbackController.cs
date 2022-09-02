using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityService.Mocks;
using QualityService.Models;
using QualityService.Models.DTO;
using QualityService.Service;
using System.Linq;

namespace QualityService.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]

    public class FeedbackController : ControllerBase
    {
        private readonly IQualityService _qualityService;
        private readonly IUserMock _user;
        private readonly IProductMock _product;

        public FeedbackController(
            IQualityService qualityService,
            IUserMock userMock,
            IProductMock productMock
            )
        {
            this._qualityService = qualityService;
            this._user = userMock;
            this._product = productMock;
        }

        // GET: Feedback
        /// <summary>
        /// Returns list of all feedback info
        /// </summary>
        /// <returns>List of feedback info</returns>
        /// <response code="200">Returns the feedback</response>
        /// <response code="401">Unauthorized feedback</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllFeedbacks()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(this._qualityService.ListAll())
            );
        }

        //GET BY ID: Feedback
        /// <summary>
        /// Returns feedback info with given id
        /// </summary>
        /// <param name="id">Feedback's Id</param>
        /// <returns> Feedback info with given id</returns>
        ///<response code="200">Returns the feedback info</response>
        /// <response code="401">Unauthorized feedback</response>
        /// <response code="404">Feedback with id is not found</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFeedback(int id)
        {
            var feedbackType = this._qualityService.GetById(id);
            if (feedbackType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(feedbackType));
        }
        //POST: Feedback
        /// <summary>
        /// Creates a new  user profile
        /// </summary>
        /// <returns>Returns the created feedback info</returns>
        /// <response code="200">Returns the created feedback</response>
        /// <response code="400">Feedback with given id doesn't exist</response>
        /// <response code="500">There was an error on the server</response>
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

            feedback.UserId = UserId;

            var newFeedback = this._qualityService.Create(feedback);

            if (newFeedback == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, new JsonResult(newFeedback));
        }

        /// <summary>
        //PUT: ReactionType
        /// Updates  user profile info
        /// </summary>
        /// <param name="id">Feedback Id</param>
        /// <param name="feedback">Feedback info</param>
        /// <returns>Confirmation that update is succesful</returns>
        /// <response code="204">Confirmation that update is succesful</response>
        /// <response code="400">Feedback with given id doesnt exist</response>
        /// <response code="401">Unauthorized feedback</response>
        /// <response code="500">Error on the server while updating</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackDTO feedback, [FromHeader] int UserId)
        {
            var currFeedback = this._qualityService.GetById(id);
            if (currFeedback == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserId) == null ) //&& UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            currFeedback = this._qualityService.Update(currFeedback, feedback);

            
            if (currFeedback == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
        //DELETE : Feedback
        /// /// <summary>
        /// Soft deletes feedback with given id
        /// </summary>
        /// <param name="id">Feedback's Id</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Feedback succesfully deleted</response>
        /// <response code="401">Unauthorized feedback</response>
        /// <response code="404">Feedback with id not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteFeedback(int id, [FromHeader] int UserID)
        {
            var currFeedback = this._qualityService.GetById(id);
            if (currFeedback == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserID) == null) // && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            
            if (!this._qualityService.Delete(currFeedback))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}
