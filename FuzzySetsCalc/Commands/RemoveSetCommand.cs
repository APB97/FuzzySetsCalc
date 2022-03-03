using FuzzySetsCalc.Services;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Commands
{
    public class RemoveSetCommand : ICommand
    {
        private FuzzySetService? _service;

        public string? RemoveId { get; set; }

        [JsonIgnore]
        public IServiceProvider? ISP
        {
            set
            {
                _service = value?.GetRequiredService<FuzzySetService>();
            }
        }

        public RemoveSetCommand(FuzzySetService? service)
        {
            _service = service;
        }

        public void Execute()
        {
            if (RemoveId == null) return;
            _service?.RemoveSet(RemoveId);
        }
    }
}
