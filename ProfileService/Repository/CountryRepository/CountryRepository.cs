using ITSolutionsCompanyV1.Repositories;
using ProfileService.Data;
using ProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository.CountryRepository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ProfileDbContext dataContext) : base(dataContext)
        {
        }
        public Country DeleteCountry(Country country)
        {
            Update(country);
            return country;
        }

        public List<Country> GetCountries()
        {
            return Query.Where<Country>(pred => pred.CountryId != null).ToList();
        }

        public Country GetCountryById(Guid id)
        {
            return FindById(id);
        }

        public void InsertCountry(Country country)
        {
            Insert(country);
        }

        public void UpdateCountry(Country country)
        {
            Update(country);
        }
    }
}
