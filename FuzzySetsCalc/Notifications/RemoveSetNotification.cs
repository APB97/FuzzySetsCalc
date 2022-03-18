using MediatR;

namespace FuzzySetsCalc.Notifications
{
    public class RemoveSetNotification : INotification
    {
        public string SetId { get; }

        public RemoveSetNotification(string setId)
        {
            SetId = setId;
        }
    }
}
