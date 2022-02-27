using System.Text.Json.Serialization;

namespace FuzzySetsCalc.Commands
{
    public interface ICommand
    {
        [JsonIgnore]
        IServiceProvider? ISP { get; set; }

        void Execute();
    }
}
