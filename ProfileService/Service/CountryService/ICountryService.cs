using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service
{
    public interface ICountryService
    {
        List<Country> GetCountries();
        Country GetCountryById(Guid id);
        void InsertCountry(Country country);
        void UpdateCountry(Country country);
        Country DeleteCountry(Guid id);
        //public Country GetCityByName(String name);
    }
}
