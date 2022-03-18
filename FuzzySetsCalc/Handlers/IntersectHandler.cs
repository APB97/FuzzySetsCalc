using FuzzySetsCalc.Notifications;
using MediatR;

namespace FuzzySetsCalc.Services
{
    public class IntersectHandler : INotificationHandler<IntersectNotification>
    {
        private readonly FuzzySetService fuzzySetService;

        public IntersectHandler(FuzzySetService fuzzySetService)
        {
            this.fuzzySetService = fuzzySetService;
        }

        public Task Handle(IntersectNotification notification, CancellationToken cancellationToken)
        {
            fuzzySetService.Intersect(notification.ResultId, notification.SetId, notification.OtherSetId);
            return Task.CompletedTask;
        }
    }
}
