using GlobalCityManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GlobalCityManager.Data
{
    public class CountryRepository : ICountryRepository
    {
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

        public void UpdateCountry(Country country)
        {
            using (var context = new worldContext())
            {
                context.Country.Update(country);
                context.SaveChanges();
            }
        }

        public void RemoveCountryByCode(string code)
        {
            using (var context = new worldContext())
            {
                var countryToDelete = new Country { Code = code };
                context.Country.Attach(countryToDelete);
                context.Country.Remove(countryToDelete);
                context.SaveChanges();
            }
        }
    }
}
