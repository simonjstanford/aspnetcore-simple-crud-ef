using GlobalCityManager.Models;
using System.Collections.Generic;

namespace GlobalCityManager.Data
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryDetails(string code);
        void CreateCountry(Country country);
        void UpdateCountry(Country country);
        void RemoveCountryByCode(string code);
    }
}
