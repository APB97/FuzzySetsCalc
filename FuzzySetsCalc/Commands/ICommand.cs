using MediatR;

namespace FuzzySetsCalc.Commands
{
    public interface ICommand
    {
        void Execute(IMediator mediator);
    }
}
