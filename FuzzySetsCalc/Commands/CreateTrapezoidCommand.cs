using FuzzySetsCalc.Models;
using FuzzySetsCalc.Services;
using System.Text.Json.Serialization;

namespace FuzzySetsCalc.Commands
{
    public class CreateTrapezoidCommand : ICommand
    {
        private FuzzySetService? _service;
        private IServiceProvider? provider;

        [JsonIgnore]
        public IServiceProvider? ISP
        {
            get => null;
            set
            {
                provider = value;
                _service = provider?.GetRequiredService<FuzzySetService>();
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
