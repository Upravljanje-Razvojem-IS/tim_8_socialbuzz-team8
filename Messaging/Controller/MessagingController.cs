using Messaging.Context;
using Messaging.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Controller
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly MessagingContext _context;

        public MessagingController(MessagingContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Returns list of all Chats
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.Chats.ToList())
            );
        }

        /// <summary>
        /// Returns specific Chat by its ID
        /// </summary>
        /// <param name="id">ID of wanted Chat</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(chat));
        }

        /// <summary>
        /// Deletes specific Chat by its ID
        /// </summary>
        /// <param name="id">ID of chat that will be deleted</param>
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
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (chat.CreatedBy != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            _context.Chats.Remove(chat);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(chat));
        }

        /// <summary>
        /// Creates new Chat
        /// </summary>
        /// <param name="chatDTO">Chat object that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ChatDTO chatDTO, [FromHeader] int UserID)
        {
            var chat = new Chat()
            {
                CreatedBy = UserID,
                Title = chatDTO.Title,
                PostId = chatDTO.PostId,
                ProductServiceId = chatDTO.ProductServiceId
            };

            _context.Chats.Add(chat);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(chat));
        }

        /// <summary>
        /// Updates specific Chat its ID
        /// </summary>
        /// <param name="id">ID of Chat that will be updated</param>
        /// <param name="chatDTO">Updated Chat that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, [FromBody] ChatDTO chatDTO, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (chat.CreatedBy != UserID && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            chat.Title = chatDTO.Title;
            chat.PostId = chatDTO.PostId;
            chat.ProductServiceId = chatDTO.ProductServiceId;

            _context.Chats.Update(chat);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(chat));
        }
    }
}
