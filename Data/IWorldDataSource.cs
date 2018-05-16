using GlobalCityManager.Models;
using System.Collections.Generic;

namespace GlobalCityManager.Data
{
    public interface IWorldRepository
    {
        IEnumerable<City> GetCities();
        IEnumerable<Country> GetCountries();
        Country GetCountryDetails(string code);
    }
}
