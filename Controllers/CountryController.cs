using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GlobalCityManager.Data;
using GlobalCityManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCityManager.Controllers
{
    public class CountryController : Controller
    {
        private IWorldRepository worldRepository;
        private IHostingEnvironment environment;

        public CountryController(IWorldRepository worldRepository, IHostingEnvironment environment)
        {
            this.worldRepository = worldRepository;
            this.environment = environment;
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
                country.NationalFlag = CreateFlag(country.Code, nationalFlagFile);
                worldRepository.CreateCountry(country);
            }

            return RedirectToAction(nameof(Index));
        }

        private string CreateFlag(string code, IFormFile nationalFlagFile)
        {
            string relativePath = string.Empty;
            if (nationalFlagFile?.Length > 0)
            {
                var targetFileName = $"{code}{Path.GetExtension(nationalFlagFile.FileName)}";
                relativePath = Path.Combine("images", targetFileName);
                var absolutePath = Path.Combine(environment.WebRootPath, relativePath);
                using (var stream = new FileStream(absolutePath, FileMode.Create))
                {
                    nationalFlagFile.CopyTo(stream);
                }
            }
            return relativePath;
        }
    }
}