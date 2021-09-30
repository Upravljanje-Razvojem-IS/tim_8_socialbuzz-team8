using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactionsService.Context;
using ReactionsService.Entity;
using ReactionsService.Entity.DTO;
using ReactionsService.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly ReactionsContext _context;
        private readonly IUserMock _user;
        private readonly IPostMock _post;

        public ReactionsController(ReactionsContext context, IUserMock user, IPostMock post)
        {
            this._context = context;
            this._user = user;
            this._post = post;
        }

        // GET: ReactionTypes
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllReactionTypes()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.ReactionTypes.ToList())
            );
        }

        //GET BY ID: ReactionTypes
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReactionType(int id)
        {
            var reactionType = _context.ReactionTypes.Find(id);
            if(reactionType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(reactionType));
        }
        //POST: ReactionTypes
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReactionType([FromBody] ReactionTypeDTO reactionType, [FromHeader] int UserId, [FromHeader] string UserRole)
        {
            if (_user.GetUserById(UserId) == null || UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var newReactionType = new ReactionType()
            {

                Name = reactionType.Name,
                Description = reactionType.Description
            };

            _context.ReactionTypes.Add(newReactionType);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, new JsonResult(newReactionType));
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
        public IActionResult UpdateReactionType(int id, [FromBody] ReactionTypeDTO reactionType, [FromHeader] int UserId, [FromHeader] string UserRole)
        {
            var currReactionType = _context.ReactionTypes.Find(id);
            if(currReactionType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserId) == null || UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            currReactionType.Name = reactionType.Name;
            currReactionType.Description = reactionType.Description;

            _context.ReactionTypes.Update(currReactionType);
            var success = _context.SaveChanges();
            if(success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
        //DELETE : Reactiontype
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteReactionType(int id, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var currReactionType = _context.ReactionTypes.Find(id);
            if(currReactionType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if(_user.GetUserById(UserID) == null || UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            _context.ReactionTypes.Remove(currReactionType);
            var success = _context.SaveChanges();
            if(success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }

        //REACTION
        //GET:
        [HttpGet]
        [Route("reaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllReactions()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new JsonResult(_context.Reactions.ToList())
            );
        }

        //POST: Reaction
        [HttpPost]
        [Route("reaction")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReaction([FromBody] ReactionDTO reaction, [FromHeader] int UserId, [FromHeader] int PostId)
        {
            if (_user.GetUserById(UserId) == null || _post.GetPostById(PostId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var checkReaction = _context.Reactions.FirstOrDefault(u => u.PostId == PostId && u.CreatedByUserId == UserId);
            if (checkReaction != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var newReaction = new Reaction()
            {
                CreatedByUserId = UserId,
                PostId = PostId,
                ReactionTypeID = reaction.ReactionTypeId
            };
            

            if (_context.ReactionTypes.Find(reaction.ReactionTypeId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            _context.Reactions.Add(newReaction);
            var success = _context.SaveChanges();

            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, new JsonResult(newReaction));
        }
        //PUT: Reaction
        [HttpPut]
        [Route("reaction/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult UpdateReaction(int id, [FromBody] ReactionDTO reaction, [FromHeader] int UserID, [FromHeader] int PostID)
        {
            var currReaction = _context.Reactions.Find(id);
            if(currReaction == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserID) == null || _post.GetPostById(PostID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if(UserID != currReaction.CreatedByUserId || PostID != currReaction.PostId)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }      
            currReaction.ReactionTypeID = reaction.ReactionTypeId;

            _context.Reactions.Update(currReaction);
            var success = _context.SaveChanges();
            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }


        //DELETE : Reaction
        [HttpDelete]
        [Route("reaction/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteReaction(int id, [FromHeader] int UserID, [FromHeader] int PostID)
        {
            var currReaction = _context.Reactions.Find(id);
            if (currReaction == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (_user.GetUserById(UserID) == null || UserID != currReaction.CreatedByUserId)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            if(_post.GetPostById(PostID) == null || PostID != currReaction.PostId)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            _context.Reactions.Remove(currReaction);
            var success = _context.SaveChanges();
            if (success < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }

        //GET REACTIONS BY POST ID
        [HttpGet]
        [Route("reactions/by-post/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReactionsByPostID(int id)
        {
            var post = _post.GetPostById(id);
            if(post == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var reactions = _context.Reactions.Where(p => p.PostId == id);
            if (reactions == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(reactions));  
        }

        //GET REACTIONS BY USER ID
        [HttpGet]
        [Route("reactions/by-user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReactionsByUserID(int id)
        {
            var user = _user.GetUserById(id);
            if(user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var reactions = _context.Reactions.Where(u => u.CreatedByUserId == id);
            if(reactions == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(reactions));
        }

        //GET REACTIONS BY REACTION TYPE
        [HttpGet]
        [Route("reactions/by-reactiontype/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReactionsByReactionType(int id)
        {
            var reactionType = _context.ReactionTypes.Find(id);
            if(reactionType == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var reactions = _context.Reactions.Where(rt => rt.ReactionTypeID == id);
            if (reactions == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(reactions));
        }

        //GET: REACTION BY ID
        [HttpGet]
        [Route("reaction/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReactionById(int id)
        {
            var reaction = _context.Reactions.Find(id);
            if (reaction == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, new JsonResult(reaction));
        }

    }
}
