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
            _storage.fuzzySets.Add(new FuzzySet
            {
                FuzzySetId = trapezoid.Id,
                MembershipFunction = trapezoid.MembershipFunction
            });
        }

        public void Intersect(string resultingSetName, string setId, string otherSetId)
        {
            var oneSet = _storage.fuzzySets.Find(f => f.FuzzySetId == setId);
            var otherSet = _storage.fuzzySets.Find(f => f.FuzzySetId == otherSetId);

            if (oneSet == null || otherSet == null)
                return;

            FuzzySet result = oneSet.Intersect(otherSet, resultingSetName);
            _storage.fuzzySets.Add(result);
        }

        public void RemoveSet(string id)
        {
            FuzzySet? foundSet = _storage.fuzzySets.Find(f => f.FuzzySetId == id);
            if (foundSet != null)
            {
                _storage.fuzzySets.Remove(foundSet);
            }
        }
    }
}
