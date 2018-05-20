using GlobalCityManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GlobalCityManager.Data
{
    public class CityRepository : ICityRepository
    {
        public IEnumerable<City> GetCities()
        {
            using (var context = new worldContext())
            {
                return context.City.Include(x => x.CountryCodeNavigation).ToList();
            }
        }

        public void CreateCity(City city)
        {
            using (var context = new worldContext())
            {
                context.City.Add(city);
                context.SaveChanges();
            }
        }

        public City GetCityDetails(int cityId)
        {
            using (var context = new worldContext())
            {
                var countryQuery = context.City.Include(db => db.CountryCodeNavigation);
                var details = countryQuery.SingleOrDefault(x => x.Id == cityId);
                return details;
            }
        }

        public void UpdateCity(City city)
        {
            using (var context = new worldContext())
            {
                context.City.Update(city);
                context.SaveChanges();
            }
        }

        public void RemoveCityById(int id)
        {
            using (var context = new worldContext())
            {
                var cityToDelete = new City { Id = id };
                context.City.Attach(cityToDelete);
                context.City.Remove(cityToDelete);
                context.SaveChanges();
            }
        }
    }
}
