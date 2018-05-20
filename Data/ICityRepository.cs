using GlobalCityManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalCityManager.Data
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task CreateCityAsync(City city);
        Task<City> GetCityDetailsAsync(int cityId);
        Task UpdateCityAsync(City city);
        Task RemoveCityByIdAsync(int id);
    }
}
