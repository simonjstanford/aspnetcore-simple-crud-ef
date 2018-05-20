using GlobalCityManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalCityManager.Data
{
    public class CountryRepository : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            using (var context = new worldContext())
            {
                return await context.Country.ToListAsync();
            }
        }

        public async Task<Country> GetCountryDetailsAsync(string countryCode)
        {
            using (var context = new worldContext())
            {

                var countryQuery = context.Country.Include(db => db.City);
                var details = await countryQuery.SingleOrDefaultAsync(x => x.Code == countryCode);
                return details;
            }
        }

        public async Task CreateCountryAsync(Country country)
        {
            await Task.Run(() =>
            {
                using (var context = new worldContext())
                {
                    context.Country.Add(country);
                    context.SaveChanges();
                }
            });
        }

        public async Task UpdateCountryAsync(Country country)
        {
            await Task.Run(() =>
            {
                using (var context = new worldContext())
                {
                    context.Country.Update(country);
                    context.SaveChanges();
                }
            });
        }

        public async Task RemoveCountryByCodeAsync(string code)
        {
            await Task.Run(() =>
            {
                using (var context = new worldContext())
                {
                    var countryToDelete = new Country { Code = code };
                    context.Country.Attach(countryToDelete);
                    context.Country.Remove(countryToDelete);
                    context.SaveChanges();
                }
            });
        }
    }
}
