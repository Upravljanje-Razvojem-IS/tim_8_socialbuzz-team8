using Messaging.Context;
using Messaging.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Messaging.Entity.DTO;

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
        public IActionResult GetByChatId(int id)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(chat));
        }

        /// <summary>
        /// Returns Chats for logged User
        /// </summary>
        [HttpGet]
        [Route("myChats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetChatsByUserId([FromHeader] int UserID)
        {
            var chats = _context.Chats.Include(chat => chat.ChatUsers).Where(chat => chat.ChatUsers.Any(chatUser => chatUser.UserId == UserID)).ToList();

            if (chats.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(chats));
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
        /// <param name="chatDto">Chat object that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ChatDto chatDto, [FromHeader] int UserID)
        {
            var chat = new Chat()
            {
                CreatedBy = UserID,
                Title = chatDto.Title,
                ChatUsers = new List<ChatUser>()
            };

            var chatUser = new ChatUser()
            {
                Chat = chat,
                RequestPending = false,
                UserId = UserID
            };

            chat.ChatUsers.Add(chatUser);

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
        /// <param name="chatDto">Updated Chat that will be saved in database</param>
        /// <param name="UserID">ID of user who sent request (automatically pulled from JWT)</param>
        /// <param name="UserRole">Role of user who sent request (automatically pulled from JWT)</param>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, [FromBody] ChatDto chatDto, [FromHeader] int UserID, [FromHeader] string UserRole)
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

            chat.Title = chatDto.Title;

            _context.Chats.Update(chat);
            
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

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(chat));
        }

        /// <summary>
        /// Returns all Users for specific Chat by its ID
        /// </summary>
        /// <param name="id">ID of Chat</param>
        [HttpGet]
        [Route("{id}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetChatUsersByChatId(int id, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();

            var currentUserIsParticipant = true;
            if (!chat.ChatUsers.Any(chatUser => chatUser.UserId == UserID))
            {
                currentUserIsParticipant = false;
            }

            if (!currentUserIsParticipant  && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var chatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == UserID);
            if (chatUser.RequestPending)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(chat.ChatUsers));
        }

        /// <summary>
        /// Adds new user to chat
        /// </summary>
        /// <param name="id">ID of Chat</param>
        [HttpPost]
        [Route("{id}/users")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddUserToChat(int id, [FromBody] ChatUserDto chatUserDto, [FromHeader] int UserID, [FromHeader] string UserRole)
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

            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();
            var newChatUser = new ChatUser()
            {
                Chat = chat,
                RequestPending = true,
                UserId = chatUserDto.UserId
            };

            chat.ChatUsers.Add(newChatUser);
            _context.Chats.Update(chat);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(chat.ChatUsers));
        }

        /// <summary>
        /// Deletes user from chat
        /// </summary>
        /// <param name="id">ID of chat</param>
        /// <param name="chatUserId">ID of User ID</param>
        [HttpDelete]
        [Route("{id}/users/{chatUserId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUserFromChat(int id, int chatUserId, [FromHeader] int UserID, [FromHeader] string UserRole)
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

            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();
            var deletedChatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == chatUserId);

            if (deletedChatUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            chat.ChatUsers.Remove(deletedChatUser);
            _context.Chats.Update(chat);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(chat.ChatUsers));
        }

        /// <summary>
        /// Accepts or rejects chat request
        /// </summary>
        /// <param name="id">ID of chat</param>
        [HttpPost]
        [Route("{id}/acceptOrReject")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AnswerToChatRequest(int id, [FromBody] TrueFalseDto answer, [FromHeader] int UserID)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();
            var chatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == UserID);

            if (chatUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            chatUser.RequestPending = !answer.value;

            if (!answer.value)
            {
                chat.ChatUsers.Remove(chatUser);
            }

            _context.Chats.Update(chat);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return StatusCode(StatusCodes.Status202Accepted, new JsonResult(answer));
        }

        /// <summary>
        /// Returns all Messages for specific Chat by its ID
        /// </summary>
        /// <param name="id">ID of Chat</param>
        [HttpGet]
        [Route("{id}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMessagesByChatId(int id, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(chat).Collection(chat => chat.ChatMessages).Load();
            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();

            var currentUserIsParticipant = true;
            if (!chat.ChatUsers.Any(chatUser => chatUser.UserId == UserID))
            {
                currentUserIsParticipant = false;
            }

            if (!currentUserIsParticipant  && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var chatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == UserID);
            if (chatUser.RequestPending)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            foreach (ChatMessage chatMessage in chat.ChatMessages)
            {
                _context.Entry(chatMessage).Collection(chatMessage => chatMessage.ChatMessageSeens).Load();
            }

            return StatusCode(StatusCodes.Status200OK, new JsonResult(chat.ChatMessages));
        }

        /// <summary>
        /// Creates new Message for specific Chat by Chat ID
        /// </summary>
        /// <param name="id">ID of Chat</param>
        [HttpPost]
        [Route("{id}/messages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateNewMessageInChat(int id, [FromBody] ChatMessageDto messageDto, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(chat).Collection(chat => chat.ChatMessages).Load();
            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();

            var currentUserIsParticipant = true;
            if (!chat.ChatUsers.Any(chatUser => chatUser.UserId == UserID))
            {
                currentUserIsParticipant = false;
            }

            if (!currentUserIsParticipant  && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var chatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == UserID);
            if (chatUser.RequestPending)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var newChatMessage = new ChatMessage()
            {
                Chat = chat,
                Userid = UserID,
                Message = messageDto.Message,
                ChatMessageSeens = new List<ChatMessageSeen>()
            };

            var newChatMessageSeen = new ChatMessageSeen()
            {
                ChatMessage = newChatMessage,
                UserId = UserID
            };

            newChatMessage.ChatMessageSeens.Add(newChatMessageSeen);

            chat.ChatMessages.Add(newChatMessage);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            foreach (ChatMessage chatMessage in chat.ChatMessages)
            {
                _context.Entry(chatMessage).Collection(chatMessage => chatMessage.ChatMessageSeens).Load();
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(chat.ChatMessages));
        }

        /// <summary>
        /// Marks message as seen by Message Id for Chat Id
        /// </summary>
        /// <param name="id">ID of Chat</param>
        /// <param name="messageId">ID of Message</param>
        [HttpPost]
        [Route("{id}/messages/{messageId}/seen")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult MarkMessageAsSeen(int id, int messageId, [FromHeader] int UserID, [FromHeader] string UserRole)
        {
            var chat = _context.Chats.Find(id);

            if (chat == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(chat).Collection(chat => chat.ChatMessages).Load();
            _context.Entry(chat).Collection(chat => chat.ChatUsers).Load();

            var currentUserIsParticipant = true;
            if (!chat.ChatUsers.Any(chatUser => chatUser.UserId == UserID))
            {
                currentUserIsParticipant = false;
            }

            if (!currentUserIsParticipant  && UserRole != "Admin")
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var chatUser = chat.ChatUsers.FirstOrDefault(chatUser => chatUser.UserId == UserID);
            if (chatUser.RequestPending)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var currentChatMessage = chat.ChatMessages.FirstOrDefault(chatMessage => chatMessage.Id == messageId);

            if (currentChatMessage == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _context.Entry(currentChatMessage).Collection(chatMessage => chatMessage.ChatMessageSeens).Load();
            var newChatMessageSeen = new ChatMessageSeen()
            {
                ChatMessage = currentChatMessage,
                UserId = UserID
            };

            currentChatMessage.ChatMessageSeens.Add(newChatMessageSeen);

            try
            {
                var success = _context.SaveChanges();

                if (success < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            foreach (ChatMessage chatMessage in chat.ChatMessages)
            {
                _context.Entry(chatMessage).Collection(chatMessage => chatMessage.ChatMessageSeens).Load();
            }

            return StatusCode(StatusCodes.Status201Created, new JsonResult(chat.ChatMessages));
        }
    }
}
