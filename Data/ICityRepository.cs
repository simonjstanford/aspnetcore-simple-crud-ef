using GlobalCityManager.Models;
using System.Collections.Generic;

namespace GlobalCityManager.Data
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();
        void CreateCity(City city);
        City GetCityDetails(int cityId);
        void UpdateCity(City city);
        void RemoveCityById(int id);
    }
}
