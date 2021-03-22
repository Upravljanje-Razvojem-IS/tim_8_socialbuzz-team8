using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserService.Dtos;
using UserService.Entities;

namespace UserService.Controllers
{

    [Route(controllerRoute)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string controllerRoute = "api/users";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;

            _configuration = configuration;
        }


        // POST: api/User
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegistrationDto registrationDto)
        {
            var userExists = await _userManager.FindByNameAsync(registrationDto.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status409Conflict, "User already exists");

            ApplicationUser newUser = new ApplicationUser(registrationDto.Username, registrationDto.Email, registrationDto.PhoneNumber, registrationDto.AccountType);

            var result = await _userManager.CreateAsync(newUser, registrationDto.Password);

            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User couldn't be created due to server error please try again in a moment");
            }

            return Ok("User created");

        }


    /*    // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
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

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteApplicationUser(Guid id)
        {
            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return applicationUser;
        }

        private bool ApplicationUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }*/
    }
}
