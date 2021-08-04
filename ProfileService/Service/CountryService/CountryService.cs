using ProfileService.Models;
using ProfileService.Repository.CountryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service
{
    public class CountryService: ICountryService
    {
        private ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Country DeleteCountry(Guid id)
        {
            Country country = _countryRepository.GetCountryById(id);
            country.Deleted = true;
            return _countryRepository.DeleteCountry(country);
        }

        public List<Country> GetCountries()
        {
           return _countryRepository.GetCountries();
        }

        public Country GetCountryById(Guid id)
        {
            return _countryRepository.GetCountryById(id);
        }

        public void InsertCountry(Country country)
        {
            _countryRepository.InsertCountry(country);
        }

        public void UpdateCountry(Country country)
        {
            _countryRepository.UpdateCountry(country);
        }
    }
}
