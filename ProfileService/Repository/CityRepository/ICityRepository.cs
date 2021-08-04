using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.CityRepository
{
    public interface ICityRepository
    {
        List<City> GetCities();
        City GetCityById(Guid id);
        void InsertCity(City city);
        void UpdateCity(City city);
        City DeleteCity(City city);
        //public City GetCityByName(String name);
    }
}
