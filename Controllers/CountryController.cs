using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCityManager.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository countryRepository;
        private IFlagUploader flagUploader;

        public CountryController(ICountryRepository worldRepository, IFlagUploader flagUploader)
        {
            this.countryRepository = worldRepository;
            this.flagUploader = flagUploader;
        }

        public IActionResult Index()
        {
            var countries = countryRepository.GetCountries();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Country country, IFormFile nationalFlagFile)
        {
            if (ModelState.IsValid)
            {
                country.NationalFlag = flagUploader.CreateFlag(country.Code, nationalFlagFile);
                countryRepository.CreateCountry(country);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(string code)
        {
            var country = countryRepository.GetCountryDetails(code);
            return View(country);
        }

        [HttpGet]
        public IActionResult Edit(string code)
        {
            var country = countryRepository.GetCountryDetails(code);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country country, IFormFile nationalFlagFile)
        {
            if (ModelState.IsValid)
            {
                if (nationalFlagFile != null)
                    country.NationalFlag = flagUploader.CreateFlag(country.Code, nationalFlagFile);
                countryRepository.UpdateCountry(country);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(Country country)
        {
            countryRepository.RemoveCountryByCode(country.Code);
            return RedirectToAction(nameof(Index));
        }
    }
}