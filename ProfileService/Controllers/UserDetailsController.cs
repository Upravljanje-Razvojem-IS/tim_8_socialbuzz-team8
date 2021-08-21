using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Contoller with endopoints for handling crud operations for user profiles 
    /// </summary>
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
        /// <summary>
        /// Returns list of all user profiles info
        /// </summary>
        /// <returns>List of all user profiles info</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
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
        /// <summary>
        /// Returns user profile info with given id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns> User profile info with given id</returns>
        ///<response code="200">Returns the user profile info</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">User profile with id is not found</response>
        /// <response code="500">Error on the server</response>
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
        /// <summary>
        /// Updates  user profile info
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="newUserDetails">New profile info</param>
        /// <returns>Confirmation that update is succesful</returns>
        /// <response code="204">Confirmation that update is succesful</response>
        /// <response code="400">UserDetails with given id doesnt exist | City with given id doesnt exist</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server while updating</response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUserDetails(Guid id, UserMutationDto newUserDetailsDto)
        {
            try
            {
                var oldUserDetails = _userDetailsService.GetUserDetailsById(id);
                if(oldUserDetails == null)
                {
                    return BadRequest("UserDetails with given id doesnt exist");
                }
                if (_cityService.GetCityById(newUserDetailsDto.CityId) == null)
                {
                    return BadRequest("City with given id doesnt exist");
                }
                var newUserDetails = _mapper.Map<UserDetails>(newUserDetailsDto);
                _userDetailsService.UpdateUserDetails(oldUserDetails, newUserDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

            return NoContent();
        }

        // POST: api/UserDetails
        /// <summary>
        /// Creates a new  user profile
        /// </summary>
        /// <param name="userDetails">Model of personal users profile</param>
        /// <returns>Returns the created personal user info</returns>
        /// <response code="200">Returns the created personal user</response>
        /// <response code="400">UserDetails with given id doesnt exist | City with given id doesnt exist</response>
        /// <response code="500">There was an error on the server</response>
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

            return CreatedAtAction("GetUserDetails", userDetails);
        }

        //// DELETE: api/UserDetails/5
        /// /// <summary>
        /// Soft deletes user profile with given id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">User succesfully deleted</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">User with userId not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
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
    }
}
