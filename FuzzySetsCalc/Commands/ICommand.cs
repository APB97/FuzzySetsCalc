using System.Text.Json.Serialization;

namespace FuzzySetsCalc.Commands
{
    public interface ICommand
    {
        [JsonIgnore]
        IServiceProvider? ISP { set; }

        void Execute();
    }
}
