using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCityManager.Controllers
{
    public class CountryController : Controller
    {
        private IWorldRepository worldRepository;

        public CountryController(IWorldRepository worldRepository)
        {
            this.worldRepository = worldRepository;
        }

        public IActionResult Index()
        {
            var countries = worldRepository.GetCountries();
            return View(countries);
        }

        public IActionResult Detail(string code)
        {
            var country = worldRepository.GetCountryDetails(code);
            return View(country);
        }
    }
}