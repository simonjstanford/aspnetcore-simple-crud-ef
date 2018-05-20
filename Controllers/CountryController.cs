using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalCityManager.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository countryRepository;
        private IFlagUploader flagUploader;

        public CountryController(ICountryRepository countryRepository, IFlagUploader flagUploader)
        {
            this.countryRepository = countryRepository;
            this.flagUploader = flagUploader;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await countryRepository.GetCountriesAsync();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Country country, IFormFile nationalFlagFile)
        {
            if (ModelState.IsValid)
            {
                country.NationalFlag = flagUploader.CreateFlag(country.Code, nationalFlagFile);
                await countryRepository.CreateCountryAsync(country);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string code)
        {
            var country = await countryRepository.GetCountryDetailsAsync(code);
            return View(country);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string code)
        {
            var country = await countryRepository.GetCountryDetailsAsync(code);
            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country country, IFormFile nationalFlagFile)
        {
            if (ModelState.IsValid)
            {
                if (nationalFlagFile != null)
                    country.NationalFlag = flagUploader.CreateFlag(country.Code, nationalFlagFile);
                await countryRepository.UpdateCountryAsync(country);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Country country)
        {
            await countryRepository.RemoveCountryByCodeAsync(country.Code);
            return RedirectToAction(nameof(Index));
        }
    }
}