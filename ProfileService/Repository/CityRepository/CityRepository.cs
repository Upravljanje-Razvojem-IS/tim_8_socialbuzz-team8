using ITSolutionsCompanyV1.Repositories;
using ProfileService.Data;
using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.CityRepository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ProfileDbContext dataContext) : base(dataContext)
        {
        }

        public City DeleteCity(City city)
        {
            Update(city);
            return city;
        }

        public List<City> GetCities()
        {
            return FindAll();
        }

        public City GetCityById(Guid id)
        {
            return FindById(id);
        }

        //public City GetCityByName(string name)
        //{
            
        //}

        public void InsertCity(City city)
        {
            Insert(city);
        }

        public void UpdateCity(City city)
        {
            Update(city);
        }
    }
}
