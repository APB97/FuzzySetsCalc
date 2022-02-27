namespace FuzzySetsCalc.Models
{
    public class FuzzySet
    {
        public string? FuzzySetId { get; set; }
        public Func<double, double>? MembershipFunction { get; set; }
    }
}
