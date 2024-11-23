using HackathonProblem.Contracts;

namespace HackathonProblem.Service.Transient;

public class HrDirector
{
    public double CalculateSatisfactionIndex(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamLeadsDesiredJuniors = teamLeadsWishlists.ToDictionary(t => t.EmployeeId, t => t.DesiredEmployees);
        var juniorsDesiredTeamLeads = juniorsWishlists.ToDictionary(j => j.EmployeeId, j => j.DesiredEmployees);

        var n = teams.Count() * 2;
        double sum = 0;

        foreach (var t in teams)
        {
            sum += 1.0 / CountSatisfaction(t.TeamLead.Id, juniorsDesiredTeamLeads[t.Junior.Id]);
            sum += 1.0 / CountSatisfaction(t.Junior.Id, teamLeadsDesiredJuniors[t.TeamLead.Id]);
        }

        return n / sum;
    }

    private static int CountSatisfaction(int employeeId, int[] desiredEmployees)
    {
        return desiredEmployees.Length - Array.FindIndex(desiredEmployees, de => de == employeeId);
    }
}