using FuzzySetsCalc.Models;
using MediatR;

namespace FuzzySetsCalc.Commands
{
    public class CreateTrapezoidNotification : INotification
    {
        public TrapezoidFuzzySet Trapezoid { get; }

        public CreateTrapezoidNotification(TrapezoidFuzzySet trapezoid)
        {
            Trapezoid = trapezoid;
        }
    }
}
