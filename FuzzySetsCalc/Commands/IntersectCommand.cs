using FuzzySetsCalc.Services;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Commands
{
    public class IntersectCommand : ICommand
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

        public string? ResultId { get; set; }
        public string? Id { get; set; }
        public string? OtherSetId { get; set; }

        public IntersectCommand(FuzzySetService? service)
        {
            _service = service;
        }

        public void Execute()
        {
            if (ResultId == null || Id == null || OtherSetId == null)
                return;
            _service?.Intersect(ResultId, Id, OtherSetId);
        }
    }
}
