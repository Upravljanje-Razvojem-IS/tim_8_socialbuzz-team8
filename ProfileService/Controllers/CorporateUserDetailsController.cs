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
    public class CorporateUserDetailsController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CorporateUserDetailsController(IUserDetailsService userDetailsService, IMapper mapper, ICityService cityService)
        {
            _userDetailsService = userDetailsService;
            _mapper = mapper;
            _cityService = cityService;
        }

        // GET: api/CorporateUserDetails
        /// <summary>
        /// Returns list of all corporate user profiles info
        /// </summary>
        /// <returns>List of all corporate user profiles info</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorporateUserDetailsDto>>> GetCorporateUserDetails()
        {
            try
            {
                var corporateUserDetails = _userDetailsService.GetCorporateUserDetails();
                return _mapper.Map<List<CorporateUserDetailsDto>>(corporateUserDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        //GET: api/CorporateUserDetails/5
        /// <summary>
        /// Returns corporate user profile info with given id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns> Corporate user profile info with given id</returns>
        ///<response code="200">Returns corporate user profile info</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">User profile with id is not found</response>
        /// <response code="500">Error on the server</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CorporateUserDetailsDto>> GetCorporateUserDetails(Guid id)
        {
            try
            {
                var corporateUserDetails = _userDetailsService.GetCorporateUserDetailsById(id);
                if (corporateUserDetails == null)
                {
                    return NotFound();
                }
                return _mapper.Map<CorporateUserDetailsDto>(corporateUserDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // PUT: api/CorporateUserDetails/5
        /// <summary>
        /// Updates  corporate user profile info
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="newUserDetails">New profile info</param>
        /// <returns>Confirmation that update is succesful</returns>
        /// <response code="204">Confirmation that update is succesful</response>
        /// <response code="400">UserDetails with given id doesnt exist | City with given id doesnt exist</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server while updating</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorporateUserDetails(Guid id, CorporateUserDetailsMutationDto newCorporateUserDetailsDto)
        {
            try
            {
                var oldCorporateUserDetails = _userDetailsService.GetCorporateUserDetailsById(id);
                if (oldCorporateUserDetails == null)
                {
                    return BadRequest("UserDetails with given id doesnt exist");
                }
                if (_cityService.GetCityById(newCorporateUserDetailsDto.CityId) == null)
                {
                    return BadRequest("City with given id doesnt exist");
                }
                var newCorporateUserDetails = _mapper.Map<CorporateUserDetails>(newCorporateUserDetailsDto);
                _userDetailsService.UpdateCorporateUserDetails(oldCorporateUserDetails, newCorporateUserDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

            return NoContent();
        }

        // POST: api/CorporateUserDetails
        /// <summary>
        /// Creates a new corporate user profile
        /// </summary>
        /// <param name="userDetails">Model of corporate users profile</param>
        /// <returns>Returns the created corporate user info</returns>
        /// <response code="200">Returns the created corporate user</response>
        /// <response code="400">UserDetails with given id doesnt exist | City with given id doesnt exist</response>
        /// <response code="500">There was an error on the server</response>
        [HttpPost]
        public async Task<ActionResult<CorporateUserDetails>> PostCorporateUserDetails(CorporateUserDetailsMutationDto corporateUserDetails)
        {
            if (_cityService.GetCityById(corporateUserDetails.CityId) == null)
            {
                return BadRequest("City with given id doesnt exist");
            }
            try
            {
                var corporateUserDetailsForCreation = _mapper.Map<CorporateUserDetails>(corporateUserDetails);
                _userDetailsService.InserCorporatetUserDetails(corporateUserDetailsForCreation);
                return CreatedAtAction("GetCorporateUserDetails", corporateUserDetailsForCreation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // DELETE: api/CorporateUserDetails/5
        /// /// <summary>
        /// Soft deletes corporate user profile with given id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">User profile succesfully deleted</response>
        /// <response code="404">User with userId not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorporateUserDetails(Guid id)
        {
            try
            {
                var corporateUserDetails = _userDetailsService.GetCorporateUserDetailsById(id);
                if (corporateUserDetails == null)
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
