using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCityManager.Controllers
{
    public class CountryController : Controller
    {
        private IWorldRepository worldRepository;
        private IFlagUploader flagUploader;

        public CountryController(IWorldRepository worldRepository, IFlagUploader flagUploader)
        {
            this.worldRepository = worldRepository;
            this.flagUploader = flagUploader;
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
                worldRepository.CreateCountry(country);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string code)
        {
            var country = worldRepository.GetCountryDetails(code);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country country, IFormFile nationalFlagFile)
        {
            if (ModelState.IsValid)
            {
                country.NationalFlag = flagUploader.CreateFlag(country.Code, nationalFlagFile);
                worldRepository.UpdateCountry(country);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}