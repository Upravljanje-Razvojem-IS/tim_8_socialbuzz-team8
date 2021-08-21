using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models.Dtos.User;
using UserService.Service.User;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;

        public ApplicationUsersController(IMapper mapper, IUsersService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        // GET: api/ApplicationUsers
        [HttpGet]
        public  ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/ApplicationUsers/userType
        [Route("{usersType}")]
        [HttpGet]
        public  ActionResult<IEnumerable<UserDto>> GetUsersByType(string usersType)
        {
            try
            {
                if (string.IsNullOrEmpty(usersType))
                {
                    var users = _userService.GetUsers();
                    return Ok(users);
                }
                else
                {
                    switch (usersType)
                    {
                        case "corporate":
                            return _userService.GetCorporateUsers();
                        case "personal":
                            return _userService.GetPersonalUsers();
                        case "admin":
                            return _userService.GetAdmins();
                        default:
                            return StatusCode(StatusCodes.Status500InternalServerError, "Invalid user type (Corporate, Personal or Admin)");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/ApplicationUsers/5
        [HttpGet("{id:Guid}")]
        public  ActionResult<UserDto> GetApplicationUser(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if(user == null)
                {
                    return NotFound("User with given id not found");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/ApplicationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  IActionResult PutApplicationUser([FromHeader] string authorization,Guid id, UserUpdateDto newUser)
        {
            try
            {
                var oldUser = _userService.GetUserById(id);
                if(oldUser == null)
                {
                    return NotFound("User with given id doesnt exist");
                }
                _userService.UpdateUser(id, newUser, authorization);
                // _userService.up
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // POST: api/ApplicationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<ApplicationUser> PostApplicationUser(UserCreateDto user)
        {
            try
            {
                var pass = user.Password;
                var userEntity = _mapper.Map<ApplicationUser>(user);
                switch (user.UserType.ToLower())
                {
                    case "corporate":
                        return _userService.CreateCorporateUser(userEntity, user.CorporateUserDetailsDto, pass);
                    case "personal":
                        return _userService.CreatePersonalUser(userEntity, user.UserDetails, pass);
                   
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Invalid user type (Corporate, Personal");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("admins")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<ApplicationUser> PostAdmin(UserCreateDto user)
        {
            try
            {
                var pass = user.Password;
                var userEntity = _mapper.Map<ApplicationUser>(user);
                return _userService.CreateAdminUser(userEntity, user.UserDetails, pass);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteApplicationUser([FromHeader] string authorization, Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound("User with given id not found");
                }
                else
                {
                    _userService.DeleteUser(id, authorization);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
