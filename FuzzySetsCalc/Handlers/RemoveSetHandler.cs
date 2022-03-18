using FuzzySetsCalc.Notifications;
using MediatR;

namespace FuzzySetsCalc.Services
{
    public class RemoveSetHandler : INotificationHandler<RemoveSetNotification>
    {
        private readonly FuzzySetService fuzzySetService;

        public RemoveSetHandler(FuzzySetService fuzzySetService)
        {
            this.fuzzySetService = fuzzySetService;
        }

        public Task Handle(RemoveSetNotification notification, CancellationToken cancellationToken)
        {
            fuzzySetService.RemoveSet(notification.SetId);
            return Task.CompletedTask;
        }
    }
}
