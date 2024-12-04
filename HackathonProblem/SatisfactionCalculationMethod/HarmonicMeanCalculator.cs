using HackathonProblem.Service.Transient;

namespace HackathonProblem.SatisfactionCalculationMethod;

public class HarmonicMeanCalculator : ISatisfactionCalculationMethod
{
    public double calculate(IEnumerable<int> values)
    {
        return values.Count() / values.Sum(v => 1.0 / v);
    }
}
