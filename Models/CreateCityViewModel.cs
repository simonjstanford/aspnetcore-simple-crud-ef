using System.Collections.Generic;

namespace GlobalCityManager.Models
{
    public class CreateCityViewModel
    {
        public City City { get; set; }
        public IEnumerable<Country> AllCountries { get; set; }
    }
}
