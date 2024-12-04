namespace HackathonProblem.Service.Transient;

public interface ISatisfactionCalculationMethod
{
    public double calculate(IEnumerable<int> values);
}
