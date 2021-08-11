using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Models;
using ProfileService.Models.Dto;
using ProfileService.Service;
using ProfileService.Service.UserDetailsService;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public UserDetailsController(IUserDetailsService userDetailsService, IMapper mapper, ICityService cityService)
        {
            _userDetailsService = userDetailsService;
            _mapper = mapper;
            _cityService = cityService;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetUserDetails()
        {
            try
            {
                var userDetails = _userDetailsService.GetUserDetails();
                return _mapper.Map<List<UserDetailsDto>>(userDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        //GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid id)
        {
            try
            {
                var userDetails = _userDetailsService.GetUserDetailsById(id);
                if(userDetails == null)
                {
                    return NotFound();
                }
                return _mapper.Map<UserDetailsDto>(userDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetails(Guid id, UserDetails newUserDetails)
        {
            try
            {
                var oldUserDetails = _userDetailsService.GetUserDetailsById(id);
                if(oldUserDetails == null)
                {
                    return BadRequest("UserDetails with given id doesnt exist");
                }
                if (_cityService.GetCityById(newUserDetails.CityId) == null)
                {
                    return BadRequest("City with given id doesnt exist");
                }
                _userDetailsService.UpdateUserDetails(oldUserDetails, newUserDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

            return NoContent();
        }

        // POST: api/UserDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserMutationDto userDetails)
        {
            if (_cityService.GetCityById(userDetails.CityId) == null)
            {
                return BadRequest("City with given id doesnt exist");
            }
            try
            {
                var userDetailsForCreation = _mapper.Map<UserDetails>(userDetails);
                _userDetailsService.InsertUserDetails(userDetailsForCreation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

            return CreatedAtAction("GetUserDetails", new { id = userDetails }, userDetails);
        }

        //// DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetails(Guid id)
        {
            try
            {
                var userDetails = _userDetailsService.GetUserDetailsById(id);
                if (userDetails == null)
                {
                    return NotFound();
                }
                _userDetailsService.DeleteUserDetails(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

            return NoContent();
        }

        //    private bool UserDetailsExists(Guid id)
        //    {
        //        return _context.UserDetails.Any(e => e.UserDetailsID == id);
        //    }
        //}
    }
}
