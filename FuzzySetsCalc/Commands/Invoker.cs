using FuzzySetsCalc.Models;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace FuzzySetsCalc.Commands
{
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.All)]
    public class Invoker
    {
        private readonly ILogger<Invoker> _logger;
        private ChartDisplaySettings? _displaySettings;

        public IList<ICommand> Commands { get; set; } = new List<ICommand>();

        public ChartDisplaySettings? DisplaySettings
        {
            get
            {
                return _displaySettings;
            }
            set
            {
                if (value == null) return;
                if (_displaySettings == null)
                {
                    _displaySettings = value;
                    return;
                }
                _displaySettings.Precision = value.Precision;
                _displaySettings.MinimumX = value.MinimumX;
                _displaySettings.MaximumX = value.MaximumX;
            }
        }

        [JsonIgnore]
        public IMediator? Mediator { get; private set; }

        public Invoker()
        {
            _logger = new NullLogger<Invoker>();
            _displaySettings = null;
        }

        public Invoker(ILogger<Invoker> logger, ChartDisplaySettings? displaySettings, IMediator? mediator)
        {
            _logger = logger;
            _displaySettings = displaySettings;
            Mediator = mediator;
        }

        public void InvokeSingle(ICommand command)
        {
            if (Mediator is not null)
            {
                command.Execute(Mediator);
                Commands.Add(command);
            }
        }

        public void InvokeAllNoThrow()
        {
            foreach (var command in Commands)
            {
                try
                {
                    if (Mediator is not null)
                    {
                        command.Execute(Mediator);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{Message}", ex.Message);
                }
            }
        }
    }
}
