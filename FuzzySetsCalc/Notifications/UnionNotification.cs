using MediatR;

namespace FuzzySetsCalc.Notifications
{
    public class UnionNotification : INotification
    {
        public string SetId { get; }
        public string OtherSetId { get; }
        public string ResultId { get; }

        public UnionNotification(string setId, string otherSetId, string resultId)
        {
            SetId = setId;
            OtherSetId = otherSetId;
            ResultId = resultId;
        }
    }
}
