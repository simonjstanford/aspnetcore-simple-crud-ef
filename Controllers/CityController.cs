using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            var cities = cityRepository.GetCities();
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreateCityViewModel();
            vm.AllCountries = countryRepository.GetCountries();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateCityViewModel createCityViewModel)
        {
            if (ModelState.IsValid)
            {
                cityRepository.CreateCity(createCityViewModel.City);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detail(int cityId)
        {
            var city = cityRepository.GetCityDetails(cityId);
            return View(city);
        }

        [HttpGet]
        public IActionResult Edit(int cityId)
        {
            var vm = new CreateCityViewModel();
            vm.City = cityRepository.GetCityDetails(cityId);
            vm.AllCountries = countryRepository.GetCountries();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                cityRepository.UpdateCity(city);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(City city)
        {
            cityRepository.RemoveCityById(city.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}