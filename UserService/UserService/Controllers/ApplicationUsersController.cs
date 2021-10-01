using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Dtos;
using UserService.Entities;
using UserService.Models.Dtos.User;
using UserService.Service.Role;
using UserService.Service.User;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;
        private readonly IRoleService _roleService;

        public ApplicationUsersController(IMapper mapper, IUsersService userService, IRoleService roleService)
        {
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
        }


        // GET: api/ApplicationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(string userType)
        {
            try
            {
                if (string.IsNullOrEmpty(userType))
                {
                    var users = _userService.GetUsers();
                    return Ok(users);
                }
                else
                {
                    switch (userType)
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
            //}

            //// GET: api/ApplicationUsers/5
            //[HttpGet("{id}")]
            //public async Task<ActionResult<ApplicationUser>> GetApplicationUser(Guid id)
            //{
            //    var applicationUser = await _context.Users.FindAsync(id);

            //    if (applicationUser == null)
            //    {
            //        return NotFound();
            //    }

            //    return applicationUser;
            //}

            //// PUT: api/ApplicationUsers/5
            //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPut("{id}")]
            //public async Task<IActionResult> PutApplicationUser(Guid id, ApplicationUser applicationUser)
            //{
            //    if (id != applicationUser.Id)
            //    {
            //        return BadRequest();
            //    }

            //    _context.Entry(applicationUser).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ApplicationUserExists(id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return NoContent();
            //}

            // POST: api/ApplicationUsers
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
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
                    case "admin":
                        return _userService.CreateAdminUser(userEntity, user.UserDetails, pass);
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, "Invalid user type (Corporate, Personal or Admin)");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //// DELETE: api/ApplicationUsers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteApplicationUser(Guid id)
        //{
        //    var applicationUser = await _context.Users.FindAsync(id);
        //    if (applicationUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(applicationUser);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ApplicationUserExists(Guid id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
