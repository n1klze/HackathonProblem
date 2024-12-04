using HackathonProblem.Contracts;

namespace HackathonProblem.Service.Transient;

public class HrDirector(ISatisfactionCalculationMethod calculator)
{
    public double CalculateSatisfactionIndex(
        IEnumerable<Team> teams,
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists
    )
    {
        var teamLeadsDesiredJuniors = teamLeadsWishlists.ToDictionary(
            t => t.EmployeeId,
            t => t.DesiredEmployees
        );
        var juniorsDesiredTeamLeads = juniorsWishlists.ToDictionary(
            j => j.EmployeeId,
            j => j.DesiredEmployees
        );

        var values = (
            from t in teams
            let teamLeadSatisfaction = CountSatisfaction(
                t.Junior.Id,
                teamLeadsDesiredJuniors[t.TeamLead.Id]
            )
            let juniorSatisfaction = CountSatisfaction(
                t.TeamLead.Id,
                juniorsDesiredTeamLeads[t.Junior.Id]
            )
            select new List<int>([teamLeadSatisfaction, juniorSatisfaction])
        ).SelectMany(e => e);

        return calculator.calculate(values);
    }

    private static int CountSatisfaction(int employeeId, int[] desiredEmployees)
    {
        return desiredEmployees.Length - Array.FindIndex(desiredEmployees, de => de == employeeId);
    }
}
