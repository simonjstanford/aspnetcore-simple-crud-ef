using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalCityManager.Controllers
{
    public class CityController : Controller
    {
        private ICityRepository cityRepository;
        private ICountryRepository countryRepository;

        public CityController(ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await cityRepository.GetCitiesAsync();
            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateCityViewModel();
            vm.AllCountries = await countryRepository.GetCountriesAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityViewModel createCityViewModel)
        {
            if (ModelState.IsValid)
            {
                await cityRepository.CreateCityAsync(createCityViewModel.City);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int cityId)
        {
            var city = await cityRepository.GetCityDetailsAsync(cityId);
            return View(city);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int cityId)
        {
            var vm = new CreateCityViewModel();
            vm.City = await cityRepository.GetCityDetailsAsync(cityId);
            vm.AllCountries = await countryRepository.GetCountriesAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(City city)
        {
            if (ModelState.IsValid)
            {
                await cityRepository.UpdateCityAsync(city);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(City city)
        {
            await cityRepository.RemoveCityByIdAsync(city.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}