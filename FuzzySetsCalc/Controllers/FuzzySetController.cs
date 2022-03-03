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

            var command = new CreateTrapezoidCommand(_fuzzySetService)
            {
                Trapezoid = trapezoid
            };
            command.Execute();
            _invoker.Commands.Add(command);
            SaveToJson("/data/commands.json");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Intersect()
        {
            return View(new IntersectionParameters { setId = "", otherSetId="", resultId = "" });
        }

        [HttpPost]
        public IActionResult Intersect(IntersectionParameters model)
        {
            var command = new IntersectCommand(_fuzzySetService)
            {
                ResultId = model.resultId,
                Id = model.setId,
                OtherSetId = model.otherSetId
            };
            command.Execute();
            _invoker.Commands.Add(command);
            SaveToJson("/data/commands.json");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var command = new RemoveSetCommand(_fuzzySetService) { RemoveId = id};
            command.Execute();
            _invoker.Commands.Add(command);
            SaveToJson("/data/commands.json");

            return RedirectToAction("Index");
        }

        protected void SaveToJson(string path)
        {
            string json = JsonConvert.SerializeObject(_invoker, _serializerSettings);
            System.IO.File.WriteAllText(path, json);
        }

        [HttpGet]
        public IActionResult Load()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Load(FormFileModel fileModel)
        {
            IFormFile? formFile = fileModel.FormFile;
            if (formFile == null) return View();

            LoadFromJsonFormFile(formFile);
            return RedirectToAction("Index");
        }

        protected void LoadFromJsonFormFile(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            
            var json = reader.ReadToEnd();
            var commands = JsonConvert.DeserializeObject<Invoker>(json, _serializerSettings)?.Commands;
            if (commands != null)
            {
                _invoker.Commands = commands;
                _invoker.InvokeAllNoThrow();
            }
        }
    }
}
