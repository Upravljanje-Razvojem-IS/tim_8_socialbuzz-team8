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

namespace ProfileService.Controllers
{
    /// <summary>
    /// Contoller with endopoints for handling crud operations for country entity 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public CountriesController(ICountryService countryService, IMapper mapper )
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        // GET: api/Countries
        /// <summary>
        /// Returns list of all countries saved in db
        /// </summary>
        /// <returns>List of countries</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = _countryService.GetCountries();
            var countryDtos = _mapper.Map<List<CountryDto>>(countries);
            return countryDtos;
        }

        // GET: api/Countries/5
        /// <summary>
        /// Returns country with a given id
        /// </summary>
        /// <param name="id">Country Id</param>
        /// <returns>Country with id</returns>
        ///<response code="200">Returns the Country</response>
        /// <response code="404">Country with id is not found</response>
        /// <response code="500">Error on the server</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CountryDto>> GetCountry(Guid id)
        {
            var country = _countryService.GetCountryById(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return countryDto;
        }

        // PUT: api/Countries/5
        /// <summary>
        /// Update's the country
        /// </summary>
        /// <param name="id">Country Id</param>
        /// <param2 name="newCity">Country info that we want updated</param>
        /// <returns>Confirmation of update</returns>
        /// <response code="204">Country updated</response>
        /// <response code="400">Country with given id doesnt exist </response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCountry(Guid id, CountryCreationDto countryDto)
        {
            var country = _countryService.GetCountryById(id);

            if (country == null)
            {
                return NotFound();
            }

            try
            {
                country.CountryName = countryDto.CountryName;
                country.Deleted = countryDto.Deleted;
                _countryService.UpdateCountry(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_countryService.GetCountryById(id) == null)
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

        // POST: api/Countries
        /// <summary>
        /// Creates a new country
        /// </summary>
        /// <param name="city">Dto model of a country</param>
        /// <returns>Confirmation of the creation of country</returns>
        /// <response code="201">Country created</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">There was an error on the server</response>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Country>> PostCountry(CountryCreationDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
             _countryService.InsertCountry(country);
            return Created("!api/Countries", country);
        }

        // DELETE: api/Countries/5
        /// <summary>
        /// Soft delte country with given id
        /// </summary>
        /// <param name="id">Country Id</param>
        /// <response code="204">Country succesfully deleted</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="400">Country with cityId not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            var country = _countryService.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryService.DeleteCountry(id);
            return NoContent();
        }

    }
}
