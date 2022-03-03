using FuzzySetsCalc.Services;
using System.Text.Json.Serialization;

namespace FuzzySetsCalc.Commands
{
    public class IntersectCommand : ICommand
    {
        private FuzzySetService? _service;
        private IServiceProvider? _provider;

        [JsonIgnore]
        public IServiceProvider? ISP
        {
            get => null;
            set
            {
                _provider = value;
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
