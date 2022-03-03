using FuzzySetsCalc.Models;
using FuzzySetsCalc.Services;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Commands
{
    public class CreateTrapezoidCommand : ICommand
    {
        private FuzzySetService? _service;

        [JsonIgnore]
        public IServiceProvider? ISP
        {
            set
            {
                _service = value?.GetRequiredService<FuzzySetService>();
            }
        }

        public CreateTrapezoidCommand(FuzzySetService? service)
        {
            _service = service;
        }

        public TrapezoidFuzzySet? Trapezoid { get; set; }

        public void Execute()
        {
            if (_service != null && Trapezoid != null)
            {
                _service.CreateTrapezoid(Trapezoid);
            }
        }
    }
}
