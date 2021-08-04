using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.CountryRepository
{
    public interface ICountryRepository
    {
        List<Country> GetCountries();
        Country GetCountryById(Guid id);
        void InsertCountry(Country country);
        void UpdateCountry(Country country);
        Country DeleteCountry(Country country);
        //public Country GetCityByName(String name);
    }
}
