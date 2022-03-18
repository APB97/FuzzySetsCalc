using FuzzySetsCalc.Notifications;
using MediatR;

namespace FuzzySetsCalc.Services
{
    public class UnionHandler : INotificationHandler<UnionNotification>
    {
        private readonly FuzzySetService fuzzySetService;

        public UnionHandler(FuzzySetService fuzzySetService)
        {
            this.fuzzySetService = fuzzySetService;
        }

        public Task Handle(UnionNotification notification, CancellationToken cancellationToken)
        {
            fuzzySetService.Union(notification.ResultId, notification.SetId, notification.OtherSetId);
            return Task.CompletedTask;
        }
    }
}
