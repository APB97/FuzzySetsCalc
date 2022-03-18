using FuzzySetsCalc.Commands;
using MediatR;

namespace FuzzySetsCalc.Services
{
    public class CreateTrapezoidHandler : INotificationHandler<CreateTrapezoidNotification>
    {
        private readonly FuzzySetService fuzzySetService;

        public CreateTrapezoidHandler(FuzzySetService fuzzySetService)
        {
            this.fuzzySetService = fuzzySetService;
        }

        public Task Handle(CreateTrapezoidNotification notification, CancellationToken cancellationToken)
        {
            fuzzySetService.CreateTrapezoid(notification.Trapezoid);
            return Task.CompletedTask;
        }
    }
}
