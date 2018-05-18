using GlobalCityManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GlobalCityManager.Data
{
    public class WorldRepository : IWorldRepository
    {
        public IEnumerable<City> GetCities()
        {
            using (var context = new worldContext())
            {
                return context.City.ToList();
            }
        }

        public IEnumerable<Country> GetCountries()
        {
            using (var context = new worldContext())
            {
                return context.Country.ToList();
            }
        }

        public Country GetCountryDetails(string countryCode)
        {
            using (var context = new worldContext())
            {

                var countryQuery = context.Country.Include(db => db.City);
                var details = countryQuery.SingleOrDefault(x => x.Code == countryCode);
                return details;
            }
        }

        public void CreateCountry(Country country)
        {
            using (var context = new worldContext())
            {
                context.Country.Add(country);
                context.SaveChanges();
            }
        }
    }
}
