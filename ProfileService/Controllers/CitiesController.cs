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
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCity()
        {
            var cities = _cityService.GetCities();
            return _mapper.Map<List<CityDto>>(cities);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(Guid id)
        {
            var city = _cityService.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            return _mapper.Map<CityDto>(city);
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(Guid id, City city)
        //{
        //    if (id != city.CityId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityDto>> PostCity(CityDtoCreation city)
        {
            var cityForCreation = _mapper.Map<City>(city);
            _cityService.InsertCity(cityForCreation);
            return Ok();
        }

        //// DELETE: api/Cities/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCity(Guid id)
        //{
        //    var city = await _context.City.FindAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.City.Remove(city);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CityExists(Guid id)
        //{
        //    return _context.City.Any(e => e.CityId == id);
        //}
    }
}