using ProfileService.Models;
using ProfileService.Repository.CityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public City DeleteCity(Guid id)
        {
            City city = _cityRepository.GetCityById(id);
            city.Deleted = true;
            return _cityRepository.DeleteCity(city);
        }

        public List<City> GetCities()
        {
            return _cityRepository.GetCities();
        }

        public City GetCityById(Guid id)
        {
            return _cityRepository.GetCityById(id);
        }

        public void InsertCity(City city)
        {
            _cityRepository.InsertCity(city);
        }

        public void UpdateCity(City city)
        {
            _cityRepository.UpdateCity(city);
        }
    }
}
