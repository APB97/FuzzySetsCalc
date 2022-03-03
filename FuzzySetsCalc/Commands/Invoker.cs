using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Commands
{
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.All)]
    public class Invoker
    {
        private readonly ILogger<Invoker> _logger;

        public IList<ICommand> Commands { get; set; } = new List<ICommand>();

        public IServiceProvider? Services { get; set; }

        public Invoker() { _logger = new NullLogger<Invoker>(); }

        public Invoker(ILogger<Invoker> logger)
        {
            _logger = logger;
        }

        public void InvokeAllNoThrow()
        {
            foreach (var command in Commands)
            {
                try
                {
                    command.ISP = Services;
                    command.Execute();
                }
                catch (Exception ex)
                {
                    _logger.LogError("{Message}", ex.Message);
                }
            }
        }
    }
}
