using FuzzySetsCalc.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuzzySetsCalc.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ChartDisplaySettings _settings;

        public SettingsController(ChartDisplaySettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var settings = new ChartDisplaySettings
            {
                MaximumX = _settings.MaximumX,
                MinimumX = _settings.MinimumX,
                Precision = _settings.Precision
            };
            return View(settings);
        }

        [HttpPost]
        public IActionResult Edit(ChartDisplaySettings settings)
        {
            if (settings == null) return View(_settings);

            _settings.MaximumX = settings.MaximumX;
            _settings.MinimumX = settings.MinimumX;
            _settings.Precision = settings.Precision;

            return RedirectToAction("Index", "FuzzySet");
        }
    }
}
