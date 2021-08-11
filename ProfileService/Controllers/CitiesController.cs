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

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, ICountryService countryService, IMapper mapper)
        {
            _cityService = cityService;
            _countryService = countryService;
            _mapper = mapper;
        }

        // GET: api/Cities
        /// <summary>
        /// Returns list of all cities saved in db
        /// </summary>
        /// <returns>List of cities</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities = _cityService.GetCities();
            try
            {
                return _mapper.Map<List<CityDto>>(cities);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // GET: api/Cities/5
        /// <summary>
        /// Returns city with a given id
        /// </summary>
        /// <param name="id">City Id</param>
        /// <returns>City with cityId</returns>
        ///<response code="200">Returns the city</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">City with cityId is not found</response>
        /// <response code="500">Error on the server</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(Guid id)
        {
            var city = _cityService.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }
            try
            {
                return _mapper.Map<CityDto>(city);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // PUT: api/Cities/5
        /// <summary>
        /// Update's the city
        /// </summary>
        /// <param name="id">City's Id</param>
        /// <param2 name="newCity">City info that we want updated</param>
        /// <returns>Confirmation of update</returns>
        /// <response code="204">City updated</response>
        /// <response code="400">City with given id doesnt exist | Country with given id doesn't exist </response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Error on the server</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id, CityMutationDto newCity)
        {
            var cityToUpdate = _cityService.GetCityById(id);

            if (cityToUpdate == null)
            {
                return BadRequest("City with given id doesnt exist");
            }

            if (_countryService.GetCountryById(newCity.CountryId) == null)
            {
                return BadRequest("Country with given id doesn't exist");
            }
            try
            {
                _cityService.UpdateCity(cityToUpdate, newCity);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }

        }

        // POST: api/Cities
        /// <summary>
        /// Creates a new city
        /// </summary>
        /// <param name="city">Dto model of a city</param>
        /// <returns>Confirmation of the creation of city</returns>
        /// <response code="201">City created</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">There was an error on the server</response>
        [HttpPost]
        public async Task<ActionResult<CityDto>> PostCity(CityMutationDto city)
        {
            var cityForCreation = _mapper.Map<City>(city);
            if (_countryService.GetCountryById(cityForCreation.CountryId) == null)
            {
                return BadRequest("Country with given id doesn't exist");
            }
            try
            {
                _cityService.InsertCity(cityForCreation);
                return Created("!api/Cities", cityForCreation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

        // DELETE: api/Cities/5
        /// <summary>
        /// Soft delte city with given id
        /// </summary>
        /// <param name="id">City Id</param>
        /// <response code="204">City succesfully deleted</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="400">City with cityId not found</response>
        /// <response code="500">Error on the server while deleting</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = _cityService.GetCityById(id);

            if (city == null)
            {
                return BadRequest("City with given id doesnt exist");
            }

            try
            {
            _cityService.DeleteCity(id);
            return NoContent();
             }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on the server");
            }
        }

    }
}