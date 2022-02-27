using FuzzySetsCalc.Services;
using System.Text.Json.Serialization;

namespace FuzzySetsCalc.Commands
{
    [JsonSerializable(typeof(ICommand))]
    public class RemoveSetCommand : ICommand
    {
        private FuzzySetService? _service;


        private IServiceProvider? provider;

        public string? RemoveId { get; set; }

        [JsonIgnore]
        public IServiceProvider? ISP
        {
            get => provider;
            set
            {
                provider = value;
                _service = provider?.GetRequiredService<FuzzySetService>();
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
