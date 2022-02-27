using FuzzySetsCalc.Data;
using FuzzySetsCalc.Models;

namespace FuzzySetsCalc.Services
{
    public class FuzzySetService
    {
        private readonly FuzzySetStorage _storage;

        public FuzzySetService(FuzzySetStorage storage)
        {
            _storage = storage;
        }

        public void CreateTrapezoid(TrapezoidFuzzySet trapezoid)
        {
            double a1 = double.PositiveInfinity, b1 = double.NaN;
            double a2 = double.NegativeInfinity, b2 = double.NaN;
            if (trapezoid.L0 != trapezoid.L1)
            {
                a1 = 1.0 / (trapezoid.L1 - trapezoid.L0);
                b1 = 1 - a1 * trapezoid.L1;
            }
            if (trapezoid.R1 != trapezoid.R0)
            {
                a2 = 1.0 / (trapezoid.R1 - trapezoid.R0);
                b2 = 1 - a2 * trapezoid.R1;
            }
            _storage.fuzzySets.Add(new FuzzySet
            {
                FuzzySetId = trapezoid.Id,
                MembershipFunction = x =>
                {
                    if (x < trapezoid.L0 || x > trapezoid.R0)
                    {
                        return 0;
                    }
                    else if (x < trapezoid.L1)
                    {
                        return a1 * x + b1;
                    }
                    else if (x < trapezoid.R1)
                    {
                        return 1;
                    }
                    else if (x < trapezoid.R0)
                    {
                        return a2 * x + b2;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
        }
    }
}
