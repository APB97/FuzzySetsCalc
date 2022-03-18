using FuzzySetsCalc.Notifications;
using MediatR;

namespace FuzzySetsCalc.Commands
{
    public class RemoveSetCommand : ICommand
    {
        public string? RemoveId { get; set; }

        public void Execute(IMediator mediator)
        {
            if (RemoveId == null) return;
            mediator.Publish(new RemoveSetNotification(RemoveId));
        }
    }
}
