using FuzzySetsCalc.Notifications;
using MediatR;

namespace FuzzySetsCalc.Commands
{
    public class IntersectCommand : ICommand
    {
        public string? ResultId { get; set; }
        public string? Id { get; set; }
        public string? OtherSetId { get; set; }

        public void Execute(IMediator mediator)
        {
            if (ResultId == null || Id == null || OtherSetId == null)
                return;
            mediator.Publish(new IntersectNotification(Id, OtherSetId, ResultId));
        }
    }
}
