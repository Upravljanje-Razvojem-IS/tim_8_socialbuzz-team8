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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountry()
        {
            var countries = _countryService.GetCountries();
            var countryDtos = _mapper.Map<List<CountryDto>>(countries);
            return countryDtos;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryCreationDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
             _countryService.InsertCountry(country);
            return Created("!api/Countries", country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
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
