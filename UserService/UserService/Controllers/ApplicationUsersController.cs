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
using UserService.Service.Role;
using UserService.Service.User;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public ApplicationUsersController(IMapper mapper, IUserService userService, IRoleService roleService)
        {
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
        }


        // GET: api/ApplicationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                if (string.IsNullOrEmpty(userType))
                {
                    List<Corporation> corporations = _coroprationUsersService.GetUsers(city, username);
                    List<PersonalUser> personalUsers = _personalUsersService.GetUsers(city, username);
                    List<UserInfoDto> users = new List<UserInfoDto>();
                    users.AddRange(_mapper.Map<List<UserInfoDto>>(personalUsers));
                    users.AddRange(_mapper.Map<List<UserInfoDto>>(corporations));
                    if (users.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(users);
                }
                else
                {
                    if (string.Equals(userType, "personalUser"))
                    {
                        List<PersonalUser> personalUsers = _personalUsersService.GetUsers(city, username);
                        if (personalUsers == null || personalUsers.Count == 0)
                        {
                            return NoContent();
                        }
                        return Ok(_mapper.Map<List<UserInfoDto>>(personalUsers));
                    }
                    else if (string.Equals(userType, "corporationUser"))
                    {
                        List<Corporation> corporations = _coroprationUsersService.GetUsers(city, username);
                        if (corporations == null || corporations.Count == 0)
                        {
                            return NoContent();
                        }
                        return Ok(_mapper.Map<List<UserInfoDto>>(corporations));
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/ApplicationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(Guid id)
        {
            var applicationUser = await _context.Users.FindAsync(id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            return applicationUser;
        }

        // PUT: api/ApplicationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(Guid id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApplicationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostApplicationUser(ApplicationUser applicationUser)
        {
            _context.Users.Add(applicationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser(Guid id)
        {
            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
