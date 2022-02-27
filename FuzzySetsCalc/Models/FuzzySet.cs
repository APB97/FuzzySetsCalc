namespace FuzzySetsCalc.Models
{
    public class FuzzySet
    {
        public string? FuzzySetId { get; set; }
        public Func<double, double> MembershipFunction { get; set; } = x => 0;

        public FuzzySet Intersect(FuzzySet otherSet, string resultingSetId)
        {
            if (otherSet == null) return this;

            return new FuzzySet() 
            {
                FuzzySetId = resultingSetId,
                MembershipFunction = x => Math.Min(this.MembershipFunction(x),otherSet.MembershipFunction(x))
            };
        }
    }
}
