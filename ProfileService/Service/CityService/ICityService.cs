using ProfileService.Models;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service
{
    public interface ICityService
    {
        List<City> GetCities();
        City GetCityById(Guid id);
        void InsertCity(City city);
        void UpdateCity(City city, City newCity);
        City DeleteCity(Guid id);
    }
}
