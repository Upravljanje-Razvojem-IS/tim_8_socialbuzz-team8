using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logger.Context;
using System.Linq;
using Logger.Entity.DTO;
using Logger.Entity;

namespace Logger.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly LoggerContext _context;

        public LoggerController(LoggerContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Returns list of all LogItems
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return StatusCode(StatusCodes.Status200OK, new JsonResult(this._context.LogItems.ToList()));
        }

        /// <summary>
        /// Returns specific LogItem by its ID
        /// </summary>
        /// <param name="id">ID of wanted LogItem</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var logItem = _context.LogItems.Find(id);

            if (logItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(logItem));
        }

        /// <summary>
        /// Deletes specific LogItem by its ID
        /// </summary>
        /// <param name="id">ID of logItem that will be deleted</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteById(int id, [FromHeader] string UserRole)
        {
            var deletedLogItem = _context.LogItems.Find(id);

            if (deletedLogItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.LogItems.Remove(deletedLogItem);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(deletedLogItem));
        }

        /// <summary>
        /// Creates new logItem
        /// </summary>
        /// <param name="logItem">logItem object that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] LoggerDto logItem, [FromHeader] int UserID)
        {
            var newLogItem = new LogItem()
            {
                Timestamp = System.DateTime.Now,
                Message = "User:" + UserID + " | Log: " + logItem.Message
            };

            _context.LogItems.Add(newLogItem);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(newLogItem));
        }

        /// <summary>
        /// Updates specific logItem by its ID
        /// </summary>
        /// <param name="id">ID of logItem that will be updated</param>
        /// <param name="logItem">Updated logItem that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, [FromBody] LoggerDto logItem, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var currentLogItem = _context.LogItems.Find(id);

            if (currentLogItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            currentLogItem.Timestamp = System.DateTime.Now;
            currentLogItem.Message = "User:" + UserID + " | Log: " + logItem.Message;

            _context.LogItems.Update(currentLogItem);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(currentLogItem));
        }
    }
}