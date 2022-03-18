using FuzzySetsCalc.Models;
using MediatR;

namespace FuzzySetsCalc.Commands
{
    public class CreateTrapezoidCommand : ICommand
    {
        public CreateTrapezoidCommand() { }

        public TrapezoidFuzzySet? Trapezoid { get; set; }

        public void Execute(IMediator mediator)
        {
            if (Trapezoid is not null)
            {
                mediator.Publish(new CreateTrapezoidNotification(Trapezoid));
            }
        }
    }
}
