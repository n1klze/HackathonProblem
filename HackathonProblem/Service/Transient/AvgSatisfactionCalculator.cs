using HackathonProblem.Database.Context;

namespace HackathonProblem.Service.Transient;

public class AvgSatisfactionCalculator(HackathonContext hackathonContext)
{
    public double calculate()
    {
        return hackathonContext.Hackathons.Sum(h => h.SatisfactionIndex)
            / hackathonContext.Hackathons.Count();
    }
}
