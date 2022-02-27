using FuzzySetsCalc.Commands;
using FuzzySetsCalc.Data;
using FuzzySetsCalc.Models;
using FuzzySetsCalc.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Controllers
{
    public class FuzzySetController : Controller
    {
        private readonly FuzzySetStorage _fuzzySetStorage;
        private readonly FuzzySetService? _fuzzySetService;
        private readonly Invoker _invoker;
        private readonly JsonSerializerSettings _serializerSettings;
        public FuzzySetController(FuzzySetStorage fuzzySetStorage, FuzzySetService fuzzySetService, Invoker invoker, JsonSerializerSettings serializerSettings)
        {
            _fuzzySetStorage = fuzzySetStorage;
            _fuzzySetService = fuzzySetService;
            _invoker = invoker;
            _serializerSettings = serializerSettings;
        }

        public IActionResult Index()
        {
            return View(_fuzzySetStorage.fuzzySets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TrapezoidFuzzySet trapezoid)
        {
            if (!ModelState.IsValid)
                return View(trapezoid);

            var command = new CreateTrapezoidCommand(_fuzzySetService);
            command.Trapezoid = trapezoid;
            command.Execute();
            _invoker.Commands.Add(command);

            string json = JsonConvert.SerializeObject(_invoker, _serializerSettings);

            System.IO.File.WriteAllText("/data/commands.json", json);

            return RedirectToAction("Index");
        }
    }
}
