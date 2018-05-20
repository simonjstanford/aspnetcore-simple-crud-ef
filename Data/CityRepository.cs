using GlobalCityManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCityManager.Data
{
    public class CityRepository : ICityRepository
    {
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            using (var context = new worldContext())
            {
                return await context.City.Include(x => x.CountryCodeNavigation).ToListAsync();
            }
        }

        public async Task CreateCityAsync(City city)
        {
            using (var context = new worldContext())
            {
                await context.City.AddAsync(city);
                context.SaveChanges();
            }
        }

        public async Task<City> GetCityDetailsAsync(int cityId)
        {
            using (var context = new worldContext())
            {
                var countryQuery = context.City.Include(db => db.CountryCodeNavigation);
                var details = await countryQuery.SingleOrDefaultAsync(x => x.Id == cityId);
                return details;
            }
        }

        public async Task UpdateCityAsync(City city)
        {
            await Task.Run(() => 
            { 
                using (var context = new worldContext())
                {
                    context.City.Update(city);
                    context.SaveChanges();
                }
            });
        }

        public async Task RemoveCityByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                using (var context = new worldContext())
                {
                    var cityToDelete = new City { Id = id };
                    context.City.Attach(cityToDelete);
                    context.City.Remove(cityToDelete);
                    context.SaveChanges();
                }
            });
        }
    }
}
