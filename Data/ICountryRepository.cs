using GlobalCityManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalCityManager.Data
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> GetCountryDetailsAsync(string code);
        Task CreateCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task RemoveCountryByCodeAsync(string code);
    }
}
