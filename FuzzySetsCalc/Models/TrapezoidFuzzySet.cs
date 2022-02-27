namespace FuzzySetsCalc.Models
{
    public class TrapezoidFuzzySet
    {
        public string? Id { get; set; }

        public double L0 { get; set; }
        public double L1 { get; set; }
        public double R1 { get; set; }
        public double R0 { get; set; }

        public Func<double, double> GetMembershipFunction()
        {
            double a1 = double.PositiveInfinity, b1 = double.NaN;
            double a2 = double.NegativeInfinity, b2 = double.NaN;
            if (L0 != L1)
            {
                a1 = 1.0 / (L1 - L0);
                b1 = 1 - a1 * L1;
            }
            if (R1 != R0)
            {
                a2 = 1.0 / (R1 - R0);
                b2 = 1 - a2 * R1;
            }
            return x =>
            {
                if (x < L0 || x > R0)
                {
                    return 0;
                }
                else if (x < L1)
                {
                    return a1 * x + b1;
                }
                else if (x < R1)
                {
                    return 1;
                }
                else if (x < R0)
                {
                    return a2 * x + b2;
                }
                else
                {
                    return 0;
                }
            };
        }
    }
}
