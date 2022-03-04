using FuzzySetsCalc.Commands;
using FuzzySetsCalc.Data;
using FuzzySetsCalc.Models;
using FuzzySetsCalc.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuzzySetsCalc.Controllers
{
    public class FuzzySetController : Controller
    {
        private readonly FuzzySetStorage _fuzzySetStorage;
        private readonly FuzzySetService _fuzzySetService;
        private readonly Invoker _invoker;
        private readonly JsonService _jsonService;

        public FuzzySetController(FuzzySetStorage fuzzySetStorage, FuzzySetService fuzzySetService, Invoker invoker, JsonService jsonService)
        {
            _fuzzySetStorage = fuzzySetStorage;
            _fuzzySetService = fuzzySetService;
            _invoker = invoker;
            _jsonService = jsonService;
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Intersect()
        {
            return View(new BinarySetOperatorParameters { SetId = "", OtherSetId="", ResultId = "" });
        }

        [HttpPost]
        public IActionResult Intersect(BinarySetOperatorParameters model)
        {
            var command = new IntersectCommand(_fuzzySetService)
            {
                ResultId = model.ResultId,
                Id = model.SetId,
                OtherSetId = model.OtherSetId
            };
            command.Execute();
            _invoker.Commands.Add(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Union()
        {
            return View(new BinarySetOperatorParameters { SetId = "", OtherSetId = "", ResultId = "" });
        }

        [HttpPost]
        public IActionResult Union(BinarySetOperatorParameters model)
        {
            var command = new UnionCommand(_fuzzySetService)
            {
                ResultId = model.ResultId,
                Id = model.SetId,
                OtherSetId = model.OtherSetId
            };
            command.Execute();
            _invoker.Commands.Add(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var command = new RemoveSetCommand(_fuzzySetService) { RemoveId = id};
            command.Execute();
            _invoker.Commands.Add(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Download()
        {
            return File(_jsonService.ToJsonByteArray(_invoker), _jsonService.JsonContentType);
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

            var deserializedInvoker = _jsonService.FromFormFile<Invoker>(formFile);
            if (deserializedInvoker == null) return View();

            _invoker.Commands = deserializedInvoker.Commands;
            _invoker.InvokeAllNoThrow();
            return RedirectToAction("Index");
        }
    }
}
